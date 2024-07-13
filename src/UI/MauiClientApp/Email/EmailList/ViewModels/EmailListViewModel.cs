using Application.Email.Features.Queries.GetEmailList;

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
        ShouldStopConversation = true;
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailModel)] = selectedEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public async Task WriteEmail()
    {
        ShouldStopConversation = true;
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    //Handler methods
    public override async Task HandleUserInteraction()
    {
        await SpeechService.SpeakAsync(UiStrings.AppInfo_Introduction);
        await SpeechService.SpeakAsync(UiStrings.AppQuery_Generic);

        //Get intent
        var userIntent = await ListenAndUserIntent();
        if (userIntent == UserIntent.WriteEmail)
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
                await SpeechService.SpeakAsync(string.Format(UiStrings.InboxInfo_EmailSummary, i + 1, message.SenderName, message.Subject));
                await SpeechService.SpeakAsync(UiStrings.InboxQuery_OpenEmail);

                //Get intent
                var userIntenti = await ListenAndUserIntent();
                if (userIntenti == UserIntent.OpenEmail || userIntenti == UserIntent.Yes)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_OpenEmail);
                    await OpenEmailCommand.ExecuteAsync(message);
                    break;
                }
            }
        }
    }
}

