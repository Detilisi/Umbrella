using Application.Email.Dtos;
using Application.Email.Features.Queries.GetEmailList;
using EmailViewModel = MauiClientApp.Email.Base.ViewModels.EmailViewModel;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator, isRootViewModel: true)
{
    //Properties
    private bool ShouldKeepConversation { get; set; }
    public ObservableCollection<EmailDto> EmailMessageList { get; set; } = [];

    //Life cycle 
    protected override async void ViewAppearing()
    {
        base.ViewAppearing();
        await LoadEmailsAsync();
    }

    //Load methods
    private async Task LoadEmailsAsync()
    {
        if (EmailMessageList.Any()) return;

        var emailListResult = await Mediator.Send(new GetEmailListQuery());
        if (emailListResult.IsFailure) return;

        foreach (var emailModel in emailListResult.Value)
        {
            EmailMessageList.Add(emailModel);
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
                await SpeechService.SpeakAsync(string.Format(UiStrings.InputReponse_ReadEmails, EmailMessageList.Count), token);

                foreach (var message in EmailMessageList)
                {
                    if (token.IsCancellationRequested) break;

                    await SpeechService.SpeakAsync(string.Format(UiStrings.InboxInfo_EmailSummary, EmailMessageList.IndexOf(message) + 1, message.SenderName, message.Subject), token);
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

