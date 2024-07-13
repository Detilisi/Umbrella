namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;

    protected bool ShouldStopConversation = false;
    protected CancellationTokenSource _cancellationTokenSource = new();

    //ViewModel lifecylce
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

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

    public override void OnViewModelClosing(CancellationToken token = default)
    {
        base.OnViewModelClosing(token);
        SpeechService.StopListenAsync(token).GetAwaiter().GetResult();
    }

    //Virtual method
    [RelayCommand]
    public virtual Task HandleUserInteraction()
    {
        return Task.CompletedTask;
    }

    //Helper method
    protected async Task<UserIntent> ListenAndUserIntent(CancellationToken token)
    {
        var userInputFailCount = 0;

        while (!token.IsCancellationRequested)
        {
            try
            {
                if (userInputFailCount == 4) OnViewModelClosing(token); //Close app

                var userInput = await SpeechService.ListenAsync(token);
                if (userInput.IsFailure)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid, token);
                    continue;
                }

                //Get intent
                var userIntent = IntentRecognizer.GetIntent(userInput.Value);
                if (userIntent == UserIntent.Undefined)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, token);
                    await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, token);
                    await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, token);
                    continue;
                }
                return userIntent;
            }
            catch (OperationCanceledException)
            {
                await SpeechService.StopListenAsync(default);
            }
        }

        return UserIntent.CancelOperation;
    }

    protected async Task<UserIntent> ListenAndUserIntent()
    {
        var userInputFailCount = 0;

        //Get user input
        start: if (!ShouldStopConversation) 
        {
            var userInput = await SpeechService.ListenAsync();
            if (userInputFailCount == 4) OnViewModelClosing(); //Close app
            if (userInput.IsFailure)
            {
                userInputFailCount++;
                await SpeechService.SpeakAsync(UiStrings.InputResponse_Invalid);
                goto start;
            }

            //Get intent
            var userIntent = IntentRecognizer.GetIntent(userInput.Value);
            if (userIntent == UserIntent.Undefined)
            {
                userInputFailCount++;
                await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined);
                await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities);
                await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart);
                goto start;
            }

            return userIntent;
        }

        return UserIntent.CancelOperation;
    }
}
