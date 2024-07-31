namespace MauiClientApp.Email.EmailDetail.ViewModels;

internal partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //View elements
    [ObservableProperty] private string sender = string.Empty;
    [ObservableProperty] private string recipient = string.Empty;
    [ObservableProperty] private string subject = string.Empty;
    [ObservableProperty] private string body = string.Empty;
    [ObservableProperty] private DateTime sentAtDate = DateTime.MinValue;

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentEmail = (EmailModel)query[nameof(EmailModel)];
        Sender = CurrentEmail.SenderName ?? CurrentEmail.Sender;
        Recipient = CurrentEmail.Recipient;
        Subject = CurrentEmail.Subject;
        Body = CurrentEmail.Body;
        SentAtDate = CurrentEmail.CreatedAt;
    }
    
    //Commands
    [RelayCommand] public static async Task ReplyEmail() => await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    
    //Handler methods
    protected override async Task ExecuteBackgroundOperation()
    {
        // Introduction
        await SpeechService.SpeakAsync(UiStrings.ReadingInfo_Introduction, ActivityToken.Token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_EmailSummary, CurrentEmail.SenderName, CurrentEmail.CreatedAt),
            ActivityToken.Token);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_Subject, CurrentEmail.Subject), ActivityToken.Token);

        // Get user input
        await SpeechService.SpeakAsync(UiStrings.ReadingQuery_ReadEmail, ActivityToken.Token);
        var captureResult = await CaptureUserInputAndIntentAsync();

        if (captureResult.Item2 == UserIntent.Yes)
        {
            bool readAgain = true;
            while (readAgain)
            {
                await SpeechService.SpeakAsync(UiStrings.ReadingReponse_ReadEmail, ActivityToken.Token);
                await SpeechService.SpeakAsync(CurrentEmail.Body, ActivityToken.Token);
                await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatRead, ActivityToken.Token);

                captureResult = await CaptureUserInputAndIntentAsync();
                readAgain = captureResult.Item2 == UserIntent.Yes;
            }
        }
        else
        {
            await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatDelete, ActivityToken.Token);
            captureResult = await CaptureUserInputAndIntentAsync();
            if (captureResult.Item2 == UserIntent.ReplyEmail)
            {
                await ReplyEmailCommand.ExecuteAsync(null);
            }
            else if (captureResult.Item2 == UserIntent.DeleteEmail)
            {
                //await DeleteEmailCommand.ExecuteAsync(null);
            }
        }
    }

}
