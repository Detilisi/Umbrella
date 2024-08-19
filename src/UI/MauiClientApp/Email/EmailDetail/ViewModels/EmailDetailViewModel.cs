namespace MauiClientApp.Email.EmailDetail.ViewModels;

internal partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator), IQueryAttributable
{
    //Properties
    private EmailDto CurrentEmail { get; set; } = null!;

    //View elements
    [ObservableProperty] private string sender = string.Empty;
    [ObservableProperty] private string recipient = string.Empty;
    [ObservableProperty] private string subject = string.Empty;
    [ObservableProperty] private string body = string.Empty;
    [ObservableProperty] private DateTime sentAtDate = DateTime.MinValue;

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentEmail = (EmailDto)query[nameof(EmailDto)];
        Sender = CurrentEmail.SenderName ?? CurrentEmail.Sender;
        Recipient = CurrentEmail.Recipient;
        Subject = CurrentEmail.Subject;
        Body = CurrentEmail.Body;
        SentAtDate = CurrentEmail.CreatedAt;
    }

    //Commands
    [RelayCommand]
    public async Task ReplyEmail()
    {
        if(CurrentEmail == null) return;

        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailDto)] = CurrentEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>(navigationParameter);
    }
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
        if (captureResult.Item2 == UserIntent.ReadEmails || captureResult.Item2 == UserIntent.OpenEmail || captureResult.Item2 == UserIntent.Yes)
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

        await SpeechService.SpeakAsync(UiStrings.ReadingQuery_Reply, ActivityToken.Token);
        captureResult = await CaptureUserInputAndIntentAsync();
        if (captureResult.Item2 == UserIntent.ReplyEmail || captureResult.Item2 == UserIntent.Yes)
        {
            await ReplyEmailCommand.ExecuteAsync(null);
        }
        else
        {
            await NavigationService.NavigateToPreviousViewModelAsync();
        }
    }

}
