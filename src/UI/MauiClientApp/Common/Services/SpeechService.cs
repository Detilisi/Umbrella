using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Globalization;

namespace MauiClientApp.Common.Services;

internal class SpeechService
{
    //Fields
    const string defaultLanguage = "en-US";
    internal static EventHandler OnSpeechRecognized { get; set; } = delegate { };

    //Fields
    private static bool _canListenExecute = true;
    private static bool _canStartListenExecute = true;
    
    private static ITextToSpeech _textToSpeech = TextToSpeech.Default;
    private static ISpeechToText _speechToText = SpeechToText.Default;

    //Permision method
    internal static void RequestPermissions(CancellationToken token = default)
    {
        var isGranted = _speechToText.RequestPermissions(token);
        _canListenExecute = isGranted.Result;
        _canStartListenExecute = isGranted.Result;
    }

    //Listen methods
    internal async Task StartListenAsync(CancellationToken token = default)
    {
        if (!_canStartListenExecute) return;
        _canStartListenExecute = false;

        await _speechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), token);
        _speechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }
    internal static async Task<string> Listen(CancellationToken cancellationToken)
    {
        _canListenExecute = false;
        var recognitionText = string.Empty;

        try
        {
            var recognitionResult = await _speechToText.ListenAsync(
                CultureInfo.GetCultureInfo(defaultLanguage),
                new Progress<string>(partialText =>
                {
                }), cancellationToken);

            if (recognitionResult.IsSuccessful)
            {
                recognitionText = recognitionResult.Text;
                //Execute handler
            }
            else
            {
                recognitionText = "Unable to recognize speech";
                await Toast.Make(recognitionResult.Exception?.Message ?? recognitionText).Show(CancellationToken.None);
            }

            return recognitionText;
        }
        finally
        {
            _canListenExecute = true;
        }
    }
    internal static async Task StopListen(CancellationToken token = default)
    {
        if (_canStartListenExecute || _canListenExecute) return;
        _canListenExecute = true;
        _canStartListenExecute = true;

        _speechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        await _speechToText.StopListenAsync(token);
    }

    //Helpers
    private static void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        var text = e.RecognitionResult;
    }
}
