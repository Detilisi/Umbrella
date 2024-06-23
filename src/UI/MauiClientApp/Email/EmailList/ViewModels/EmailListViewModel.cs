using Application.Email.Features.Queries.GetEmailList;
using MauiClientApp.Common.Enums;
using MauiClientApp.Resources.Strings;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Properties
    private bool ShouldKeepConversation { get; set; }
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);
        await LoadEmailsAsync(token);
        await StartVMConversationm();
    }

    //Load methods
    private async Task LoadEmailsAsync(CancellationToken token)
    {
        var loadEmailQuery = new GetEmailListQuery(1);
        var emailList = await _mediator.Send(loadEmailQuery, token);
        if (emailList.IsFailure) return;

        EmailMessageList.Clear();
        foreach (var emailModel in emailList.Value)
        {
            EmailMessageList.Add(emailModel);
        }
    }

    //Commands
    [RelayCommand]
    public async Task OpenEmail(EmailModel selectedEmail)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailModel)] = selectedEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public async Task WriteEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    //VM conversationm
    private async Task StartVMConversationm()
    {
        var userInputFailCount = 0;
        await SpeechService.SpeakAsync(UiStrings.AppInfo_Introduction);
        await SpeechService.SpeakAsync(UiStrings.AppQuery_Generic);

        //Get user input
        start : var userInput = await SpeechService.ListenAsync();
        if (userInput.IsFailure) 
        {
            userInputFailCount++;
            if (userInputFailCount == 4) OnViewModelClosing(); //Close app
            
            await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid);
            goto start;
        }

        //Get intent
        var userIntent = IntentRecognizer.GetIntent(userInput.Value);
        if (userIntent == UserIntent.Undefined)
        {
            await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined);
            await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities);
            await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart);
            goto start;
        }
        else if (userIntent == UserIntent.WriteEmail)
        {
            await SpeechService.SpeakAsync(UiStrings.InputReponse_WriteEmail);
            await WriteEmailCommand.ExecuteAsync(null);
        }
        else if (userIntent == UserIntent.ReadEmails)
        {
            await SpeechService.SpeakAsync(string.Format(UiStrings.InputReponse_ReadEmails, EmailMessageList.Count));
            //Skim through all message
            for (var i = 0; i < EmailMessageList.Count; i++)
            {
                var message = EmailMessageList[i];
                await SpeechService.SpeakAsync(string.Format(UiStrings.InboxInfo_EmailSummarry, i + 1, message.SenderName, message.Subject));
                await SpeechService.SpeakAsync(UiStrings.InboxQuery_OpenEmail);
                var userInput1 = await SpeechService.ListenAsync();
                var userIntent1 = IntentRecognizer.GetIntent(userInput1.Value);
                if (userIntent1 == UserIntent.OpenEmail || userIntent1 == UserIntent.Affirm)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_OpenEmail);
                    await OpenEmailCommand.ExecuteAsync(message);
                    break;
                }
            }
        }
    }
}

