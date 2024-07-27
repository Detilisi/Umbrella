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
    protected override async Task ExecuteBackgroundOperation()
    {
        // Introduction
        await SpeechService.SpeakAsync(UiStrings.ReadingInfo_Introduction, CancellationTokenSource.Token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_EmailSummary, CurrentEmail.SenderName, CurrentEmail.CreatedAt), 
            CancellationTokenSource.Token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_Subject, CurrentEmail.Subject), CancellationTokenSource.Token);

        // Get user input
        await SpeechService.SpeakAsync(UiStrings.ReadingQuery_ReadEmail, CancellationTokenSource.Token);
        var captureResult = await CaptureUserInputAndIntentAsync();

        if (captureResult.Item2 == UserIntent.Yes)
        {
            bool readAgain = true;
            while (readAgain)
            {
                await SpeechService.SpeakAsync(UiStrings.ReadingReponse_ReadEmail, CancellationTokenSource.Token);
                await SpeechService.SpeakAsync(CurrentEmail.Body, CancellationTokenSource.Token);
                await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatRead, CancellationTokenSource.Token);

                captureResult = await CaptureUserInputAndIntentAsync();
                readAgain = captureResult.Item2 == UserIntent.Yes;
            }
        }
        else
        {
            await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatDelete, CancellationTokenSource.Token);
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
