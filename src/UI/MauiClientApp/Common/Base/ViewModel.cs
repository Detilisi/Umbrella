using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Diagnostics;
using System.Globalization;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    //Services
    private readonly ITextToSpeech _textToSpeech;
    private readonly ISpeechToText _speechToText;
    private const string defaultLanguage = "en-US";
    
    //Properties
    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(StartListenCommand))]
    bool canStartListenExecute = true;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(StopListenCommand))]
    bool canStopListenExecute = false;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //Construction
    protected ViewModel()
    {
        _textToSpeech = TextToSpeech.Default;
        _speechToText = SpeechToText.Default;
    }

    public virtual async void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
        await StartListenCommand.ExecuteAsync(token);
    }
    public virtual void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
        StopListenCommand.Execute(token);
    }

    //Method
    [RelayCommand]
    internal async Task Speak(string text, CancellationToken token = default)
    {
        var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            ChatHistory.Add(new ChatHistoryModel()
            {
                Sender = ChatSender.Bot,
                Message = text
            });

            await _textToSpeech.SpeakAsync(text, new()
            {
                Pitch = 1,
                Volume = 1
            }, token).WaitAsync(timeoutCancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            await Toast.Make("Playback automatically stopped after 5 seconds").Show(token);
        }
    }

    [RelayCommand(CanExecute = nameof(CanStartListenExecute))]
    internal async Task StartListen()
    {
        CanStartListenExecute = false;
        CanStopListenExecute = true;

        var isGranted = await _speechToText.RequestPermissions();
        if (!isGranted)
        {
            await Toast.Make("Permission not granted").Show();
            return;
        }

        await _speechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage));

        _speechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }

    [RelayCommand(CanExecute = nameof(CanStopListenExecute))]
    internal Task StopListen(CancellationToken cancellationToken)
    {
        CanStartListenExecute = true;
        CanStopListenExecute = false;
        _speechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        return _speechToText.StopListenAsync(cancellationToken);
    }

    //Event handlers
    private async void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        var recognitionText = e.RecognitionResult;
        ChatHistory.Add(new ChatHistoryModel() 
        { 
            Sender = ChatSender.Human, 
            Message = recognitionText
        });

        await SpeakCommand.ExecuteAsync(recognitionText);
    }
}