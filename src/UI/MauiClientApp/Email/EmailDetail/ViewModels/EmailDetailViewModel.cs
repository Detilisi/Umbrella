namespace MauiClientApp.Email.EmailDetail.ViewModels;

internal partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        CurrentEmail = selectedEmail;
    }

    //Commands
    [RelayCommand]
    public static async Task ReplyEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    [RelayCommand]
    public static async Task DeleteEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    //Handler methods
    protected override async Task HandleUserInteractionAsync()
    {
        var token = _cancellationTokenSource.Token;

        // Introduction
        await SpeechService.SpeakAsync(UiStrings.ReadingInfo_Introduction, token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_EmailSummary, CurrentEmail.SenderName, CurrentEmail.CreatedAt), token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_Subject, CurrentEmail.Subject), token);

        // Get user input
        await SpeechService.SpeakAsync(UiStrings.ReadingQuery_ReadEmail, token);
        var captureResult = await CaptureUserInputAndIntentAsync();

        if (captureResult.Item2 == UserIntent.Yes)
        {
            bool readAgain = true;
            while (readAgain)
            {
                await SpeechService.SpeakAsync(UiStrings.ReadingReponse_ReadEmail, token);
                await SpeechService.SpeakAsync(CurrentEmail.Body, token);
                await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatRead, token);

                captureResult = await CaptureUserInputAndIntentAsync();
                readAgain = captureResult.Item2 == UserIntent.Yes;
            }
        }
        else
        {
            await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatDelete, token);
            captureResult = await CaptureUserInputAndIntentAsync();
            if (captureResult.Item2 == UserIntent.ReplyEmail)
            {
                await ReplyEmailCommand.ExecuteAsync(null);
            }
            else if (captureResult.Item2 == UserIntent.DeleteEmail)
            {
                await DeleteEmailCommand.ExecuteAsync(null);
            }
        }
    }

}
