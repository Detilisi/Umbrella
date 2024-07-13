using Application.Email.Features.Queries.GetEmailList;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Properties
    private bool ShouldKeepConversation { get; set; }
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    //Life cycle 
    public override async void OnViewModelStarting()
    {
        base.OnViewModelStarting();
        await LoadEmailsAsync();
    }

    //Load methods
    private async Task LoadEmailsAsync()
    {
        var loadEmailQuery = new GetEmailListQuery(1);
        var emailList = await _mediator.Send(loadEmailQuery);
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
        _cancellationTokenSource.Cancel();
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailModel)] = selectedEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public async Task WriteEmail()
    {
        _cancellationTokenSource.Cancel();
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    //Handler methods
    public override async Task HandleUserInteractionAsync()
    {
        var token = _cancellationTokenSource.Token;
        await SpeechService.SpeakAsync(UiStrings.AppInfo_Introduction, token);
        await SpeechService.SpeakAsync(UiStrings.AppQuery_Generic, token);

        //Get intent
        var userIntent = await ListenAndUserIntent();
        if (userIntent == UserIntent.WriteEmail)
        {
            await SpeechService.SpeakAsync(UiStrings.InputReponse_WriteEmail, token);
            await WriteEmailCommand.ExecuteAsync(null);
        }
        else if (userIntent == UserIntent.ReadEmails)
        {
            await SpeechService.SpeakAsync(string.Format(UiStrings.InputReponse_ReadEmails, EmailMessageList.Count), token);

            //Skim through all message
            for (var i = 0; i < EmailMessageList.Count; i++)
            {
                if(token.IsCancellationRequested) break;

                var message = EmailMessageList[i];
                await SpeechService.SpeakAsync(string.Format(UiStrings.InboxInfo_EmailSummary, i + 1, message.SenderName, message.Subject), token);
                await SpeechService.SpeakAsync(UiStrings.InboxQuery_OpenEmail, token);

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

