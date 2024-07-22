﻿using Android.Media;

namespace Application.Email.Base;

internal partial class EmailViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;
    protected CancellationTokenSource _cancellationTokenSource = new();

    //Properties
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
        var token = _cancellationTokenSource.Token;

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
                if (userIntent == UserIntent.Undefined)
                {
                    userInputFailCount++;
                    await SpeechService.SpeakAsync(UiStrings.InputResponse_Undefined, token);
                    await SpeechService.SpeakAsync(UiStrings.AppInfo_Capabilities, token);
                    await SpeechService.SpeakAsync(UiStrings.AppCommand_Restart, token);
                    continue;
                }
                else if (userIntent == UserIntent.GoBack || userIntent == UserIntent.Cancel) 
                {
                    await SpeechService.SpeakAsync("Current operation terminated.", token);
                    await NavigationService.NavigateToPreviousViewModelAsync();
                }

                return userIntent;
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
