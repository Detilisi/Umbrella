namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator, bool isRootViewModel = false) : ViewModel(mediator, isRootViewModel)
{
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

    protected override async void ViewNavigatedTo()
    {
        base.ViewNavigatedTo();
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
        while (!ActivityToken.Token.IsCancellationRequested)
        {
            try
            {
                IsListening = true;
                var userInputResult = await SpeechService.ListenAsync(ActivityToken.Token);
                IsListening = false;

                if (userInputResult.IsFailure)
                {
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, ActivityToken.Token);
                    continue;
                }

                //Get intent
                var userText = userInputResult.Value;
                var userIntent = IntentRecognizer.GetIntent(userText);

                switch (userIntent)
                {
                    case UserIntent.Undefined:
                        if (ignoreUndefinedIntent) return Tuple.Create(userText, userIntent);
                        
                        await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, ActivityToken.Token);
                        await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, ActivityToken.Token);
                        await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, ActivityToken.Token);
                        break;

                    case UserIntent.GoBack or UserIntent.Cancel:
                        await SpeechService.SpeakAsync(UiStrings.AppResponse_Cancel, ActivityToken.Token);
                        
                        ViewDisappearing();
                        return Tuple.Create(userText, userIntent);

                    default:
                        return Tuple.Create(userText, userIntent);
                }
            }
            catch 
            {
                await SpeechService.SpeakAsync(UiStrings.AppInfo_GenericError, ActivityToken.Token);
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
