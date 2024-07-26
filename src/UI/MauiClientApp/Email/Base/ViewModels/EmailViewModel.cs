namespace MauiClientApp.Email.Base.ViewModels;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator Mediator = mediator;
    protected CancellationTokenSource CancellationTokenSource = new();

    //Properties
    [ObservableProperty] internal static bool isListening;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecycle
    public override void OnViewModelStarting()
    {
        IsListening = false;
        base.OnViewModelStarting();

        if (SpeechService.OnSpeechAnounced != null && SpeechService.OnSpeechRecognized != null) return;
        SpeechService.OnSpeechAnounced = text => ChatHistory.Add(new ChatHistoryModel()
        {
            Sender = ChatSender.Bot,
            Message = text
        });
        SpeechService.OnSpeechRecognized = recognitionText => ChatHistory.Add(new ChatHistoryModel()
        {
            Sender = ChatSender.Human,
            Message = recognitionText
        });
    }

    public override void OnViewModelClosing()
    {
        CancellationTokenSource.Cancel();

        base.OnViewModelClosing();
        SpeechService.StopListenAsync().GetAwaiter().GetResult();
    }

    public override async void OnViewModelHasFocus()
    {
        base.OnViewModelHasFocus();

        CancellationTokenSource = new();
        await HandleUserInteractionAsync();

    }

    //Virtual method
    public virtual Task HandleUserInteractionAsync()
    {
        return Task.CompletedTask;
    }

    //Helper method
    protected async Task<Result<string>> ListenAsync(CancellationToken token = default)
    {
        IsListening = true;
        var result = await SpeechService.ListenAsync(token);
        IsListening = false;
        return result;
    }
    protected async Task<UserIntent> ListenForUserIntent()
    {
        var userInputFailCount = 0;
        var token = CancellationTokenSource.Token;

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (userInputFailCount == 4) OnViewModelClosing(); //Close app

                var userInput = await ListenAsync(token);
                if (userInput.IsFailure)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, token);
                    continue;
                }

                //Get intent
                var userIntent = IntentRecognizer.GetIntent(userInput.Value);
                if (userIntent != UserIntent.Undefined) return userIntent;

                userInputFailCount++;
                await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, token);
                await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, token);
                await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, token);
                continue;
            }
            catch (OperationCanceledException)
            {
                await SpeechService.StopListenAsync(default);
            }
        }
        IsListening = false;
        return UserIntent.CancelOperation;
    }
}
