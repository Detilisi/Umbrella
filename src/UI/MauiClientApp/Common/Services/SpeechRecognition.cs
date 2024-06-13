using CommunityToolkit.Maui.Media;
using System.Globalization;

namespace MauiClientApp.Common.Services;

internal class SpeechRecognition
{
    //Fields
    const string defaultLanguage = "en-US";

    //Fields
    private bool _canStartListenExecute = true;
    private static ISpeechToText _speechToText = SpeechToText.Default;

    //Permision method
    public void RequestPermissions(CancellationToken cancellationToken = default)
    {
        _speechToText.RequestPermissions(cancellationToken);
    }

    //Listen methods
    public async Task StartListenAsync(CancellationToken token = default)
    {
        if (!_canStartListenExecute) return;
        _canStartListenExecute = false;

        await _speechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), token);
        _speechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }
    public async Task StopListen(CancellationToken token = default)
    {
        if (_canStartListenExecute) return;
        _canStartListenExecute = true;

        _speechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        await _speechToText.StopListenAsync(token);
    }

    //Helpers
    private void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        var text = e.RecognitionResult;
    }
}
