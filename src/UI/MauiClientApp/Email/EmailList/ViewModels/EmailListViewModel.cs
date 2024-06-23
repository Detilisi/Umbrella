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
        await SpeechService.SpeakAsync("Hello my name is Umbrella, your voice operated emailing system.");
        await SpeechService.SpeakAsync("How can I help you today?");

        //Get user input
        start : var userInput = await SpeechService.ListenAsync();
        if (userInput.IsFailure) 
        {
            userInputFailCount++;
            if (userInputFailCount == 4) OnViewModelClosing(); //Close app
            
            await SpeechService.SpeakAsync("Sorry, I don't think I understood that, may you please try again.");
            goto start;
        }

        //Get intent
        var userIntent = IntentRecognizer.GetIntent(userInput.Value);
        if (userIntent == UserIntent.Undefined)
        {
            await SpeechService.SpeakAsync("Sorry, I cannot perform this request at the moment.");
            await SpeechService.SpeakAsync("My capabilities are only limited to reading or writing email messages.");
            await SpeechService.SpeakAsync("Please try again.");
            goto start;
        }
        else if (userIntent == UserIntent.WriteEmail)
        {
            await SpeechService.SpeakAsync("Sure thing, I will navigate to the email drafting screen and we will get started from there.");
            await WriteEmailCommand.ExecuteAsync(null);
        }
        else if (userIntent == UserIntent.ReadEmails)
        {
            await SpeechService.SpeakAsync($"Sure thing, you have {EmailMessageList.Count} unread messages");
            //Skim through all message
            for (var i = 0; i < EmailMessageList.Count; i++)
            {
                var message = EmailMessageList[i];
                await SpeechService.SpeakAsync(string.Format(UiStrings.GlanceOverEmailMessage, i + 1, message.SenderName, message.Subject));
                await SpeechService.SpeakAsync("Do you want me to open this email?");
                var userInput1 = await SpeechService.ListenAsync();
                var userIntent1 = IntentRecognizer.GetIntent(userInput1.Value);
                if (userIntent1 == UserIntent.OpenEmail || userInput1.Value.ToLower() == "yes")
                {
                    await SpeechService.SpeakAsync("No problem, I will navigate to the email detail screen and we will get started from there.");
                    await OpenEmailCommand.ExecuteAsync(message);
                    break;
                }
            }
        }

    }
}

