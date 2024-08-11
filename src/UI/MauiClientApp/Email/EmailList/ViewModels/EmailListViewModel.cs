using Application.Email.Features.Queries.GetEmailList;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator, isRootViewModel: true)
{
    //Properties
    public ObservableCollection<EmailDto> EmailList { get; set; } = [];

    //Life cycle 
    protected override async void ViewAppearing()
    {
        base.ViewAppearing();
        await LoadEmailsAsync();
    }

    //Load methods
    private async Task LoadEmailsAsync()
    {
        if (EmailList.Any()) return;

        var emailListResult = await Mediator.Send(new GetEmailListQuery());
        if (emailListResult.IsFailure) return;

        foreach (var emailModel in emailListResult.Value)
        {
            EmailList.Add(emailModel);
        }
    }

    //Commands
    [RelayCommand]
    public async Task OpenEmail(EmailDto selectedEmail)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailDto)] = selectedEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public static async Task WriteEmail() => await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();

    [RelayCommand]
    public static async Task ViewContacts() => await NavigationService.NavigateToViewModelAsync<ContactsListViewModel>();

    //Handler methods
    protected override async Task ExecuteBackgroundOperation()
    {
        var token = ActivityToken.Token;
        await SpeechService.SpeakAsync(UiStrings.AppInfo_Introduction, token);
        await SpeechService.SpeakAsync(UiStrings.AppQuery_Generic, token);

        //Get intent
        var captureResult = await CaptureUserInputAndIntentAsync();
        switch (captureResult.Item2)
        {
            case UserIntent.WriteEmail:
                await SpeechService.SpeakAsync(UiStrings.InputReponse_WriteEmail, token);
                await WriteEmailCommand.ExecuteAsync(null);
                break;
            case UserIntent.ReadEmails:
                await SpeechService.SpeakAsync(string.Format(UiStrings.InputReponse_ReadEmails, EmailList.Count), token);

                foreach (var message in EmailList)
                {
                    if (token.IsCancellationRequested) break;

                    await SpeechService.SpeakAsync(string.Format(UiStrings.InboxInfo_EmailSummary, EmailList.IndexOf(message) + 1, message.SenderName, message.Subject), token);
                    await SpeechService.SpeakAsync(UiStrings.InboxQuery_OpenEmail, token);

                    captureResult = await CaptureUserInputAndIntentAsync();
                    if (captureResult.Item2 == UserIntent.OpenEmail || captureResult.Item2 == UserIntent.Yes)
                    {
                        await SpeechService.SpeakAsync(UiStrings.InputResponse_OpenEmail);
                        await OpenEmailCommand.ExecuteAsync(message);
                        break;
                    }
                    else if (captureResult.Item2 == UserIntent.WriteEmail)
                    {
                        await SpeechService.SpeakAsync(UiStrings.InputReponse_WriteEmail, token);
                        await WriteEmailCommand.ExecuteAsync(null);
                        break;
                    }
                }
                break;
            default:
                //TO DO: Handle unexpected intents
                break;
        }

    }
}

