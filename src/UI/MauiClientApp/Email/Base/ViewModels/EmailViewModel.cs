namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator Mediator = mediator;
    protected CancellationTokenSource CancellationTokenSource = new();

    //Properties
    [ObservableProperty] internal static bool isListening;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //ViewModel lifecylce
    protected override void ViewAppearing()
    {
        IsListening = false;
        base.ViewAppearing();

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

    protected override void ViewDisappearing()
    {
        //CancellationTokenSource.Cancel();

        base.ViewDisappearing();
    }

    protected override async void ViewNavigatedTo()
    {
        base.ViewNavigatedTo();

        CancellationTokenSource = new();
        //await ExecuteBackgroundOperation();

    }

    //Virtual method
    protected virtual Task ExecuteBackgroundOperation()
    {
        return Task.CompletedTask;
    }

    //Helper method
    protected async Task<Tuple<string, UserIntent>> CaptureUserInputAndIntentAsync(bool ignoreUndefinedIntent = false)
    {
        while (!CancellationTokenSource.Token.IsCancellationRequested)
        {
            try
            {
                IsListening = true;
                var userInputResult = await SpeechService.ListenAsync(CancellationTokenSource.Token);
                IsListening = false;

                if (userInputResult.IsFailure)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, CancellationTokenSource.Token);
                    continue;
                }

                //Get intent
                var userText = userInputResult.Value;
                var userIntent = IntentRecognizer.GetIntent(userText);

                switch (userIntent)
                {
                    case UserIntent.Undefined:
                        if (ignoreUndefinedIntent) return Tuple.Create(userText, userIntent);
                        
                        await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, CancellationTokenSource.Token);
                        await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, CancellationTokenSource.Token);
                        await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, CancellationTokenSource.Token);
                        break;

                    case UserIntent.GoBack or UserIntent.Cancel:
                        await SpeechService.SpeakAsync(UiStrings.AppResponse_Cancel, CancellationTokenSource.Token);
                        
                        ViewDisappearing();
                        return Tuple.Create(userText, userIntent); // Early return

                    default:
                        return Tuple.Create(userText, userIntent);
                }
            }
            catch 
            {
                await SpeechService.SpeakAsync(UiStrings.AppInfo_GenericError, CancellationTokenSource.Token);
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
