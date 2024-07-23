namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;
    protected CancellationTokenSource _cancellationTokenSource = new();

    //Properties
    internal bool HasPreviousViewModel { get; set; }
    [ObservableProperty] internal static bool isListening;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecylce
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
        _cancellationTokenSource.Cancel();

        base.OnViewModelClosing();
        SpeechService.StopListenAsync().GetAwaiter().GetResult();
    }

    public override async void OnViewModelHasFocus()
    {
        base.OnViewModelHasFocus();

        _cancellationTokenSource = new();
        await HandleUserInteractionAsync();

    }

    //Virtual method
    public virtual Task HandleUserInteractionAsync()
    {
        return Task.CompletedTask;
    }

    //Helper method
    protected async Task<Tuple<string, UserIntent>> CaptureUserInputAndIntentAsync()
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            try
            {
                IsListening = true;
                var userInputResult = await SpeechService.ListenAsync(_cancellationTokenSource.Token);
                IsListening = false;

                if (userInputResult.IsFailure)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, _cancellationTokenSource.Token);
                    continue;
                }

                //Get intent
                var userText = userInputResult.Value;
                var userIntent = IntentRecognizer.GetIntent(userText);
                if (userIntent == UserIntent.Undefined)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, _cancellationTokenSource.Token);
                    await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, _cancellationTokenSource.Token);
                    await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, _cancellationTokenSource.Token);
                    continue;
                }
                else if (userIntent == UserIntent.GoBack || userIntent == UserIntent.Cancel)
                {
                    await SpeechService.SpeakAsync(UiStrings.AppResponse_Cancel, _cancellationTokenSource.Token);
                    OnViewModelClosing();

                    if (HasPreviousViewModel) await NavigationService.NavigateToPreviousViewModelAsync();
                }

                return Tuple.Create(userText, userIntent);
            }
            catch 
            {
                await SpeechService.SpeakAsync(UiStrings.AppInfo_GenericError, _cancellationTokenSource.Token);
                continue;
            }
            finally
            {
                IsListening = false;
                await SpeechService.StopListenAsync(default);
            }
        }

        return Tuple.Create(string.Empty, UserIntent.Undefined);
    }
}
