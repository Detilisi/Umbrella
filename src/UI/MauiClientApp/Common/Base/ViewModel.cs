using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Diagnostics;
using System.Globalization;

namespace MauiClientApp.Common.Base;

internal abstract partial class ViewModel : ObservableObject
{
    private const string defaultLanguage = "en-US";
    internal string RecognitionText = string.Empty;

    //Booleans
    private bool _canStopListenExecute;
    private bool _canStartListenExecute = true;
    
    //Services
    private readonly ITextToSpeech _textToSpeech;
    private readonly ISpeechToText _speechToText;

    //Other
    internal event EventHandler OnSpeechRecognized = null!;
    internal static ObservableCollection<ChatHistoryModel> ChatHistory { get; private set; } = [];

    //Construction
    protected ViewModel()
    {
        _textToSpeech = TextToSpeech.Default;
        _speechToText = SpeechToText.Default;
    }

    public virtual void OnViewModelStarting(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }
    public virtual void OnViewModelClosing(CancellationToken token = default)
    {
        Debug.WriteLine($"{GetType().Name} is closing");
    }

    //Method
    internal async Task Speak(string text, CancellationToken token = default)
    {
        var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
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

    internal async Task StartListen(CancellationToken token = default)
    {
        _canStartListenExecute = false;
        _canStopListenExecute = true;

        var isGranted = await _speechToText.RequestPermissions(token);
        if (!isGranted)
        {
            await Toast.Make("Permission not granted").Show(token);
            return;
        }

        await _speechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), token);

        _speechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }

    Task StopListen(CancellationToken cancellationToken)
    {
        _canStartListenExecute = true;
        _canStopListenExecute = false;
        _speechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        return _speechToText.StopListenAsync(cancellationToken);
    }

    //Event handlers
    async void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        RecognitionText = e.RecognitionResult;
        await Speak(RecognitionText);
    }

    async void HandleRecognitionResultCompleted(object? sender, SpeechToTextRecognitionResultCompletedEventArgs e)
    {
        RecognitionText = e.RecognitionResult;
        await Speak(RecognitionText);
    }

    async void HandleSpeechToTextStateChanged(object? sender, SpeechToTextStateChangedEventArgs e)
    {
        await Toast.Make($"State Changed: {e.State}").Show(CancellationToken.None);
    }
}