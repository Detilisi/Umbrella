using CommunityToolkit.Maui.Media;
using System.Globalization;

namespace MauiClientApp.Common.Services;

internal class SpeechRecognition
{
    //Fields
    const string defaultLanguage = "en-US";

    //Fields
    private bool _canStartListenExecute;
    private static ISpeechToText _speechToText = SpeechToText.Default;

    //Permision method
    public void RequestPermissions(CancellationToken cancellationToken = default)
    {
        _speechToText.RequestPermissions(cancellationToken);
    }

    //Listen methods
    public async Task StartListen(CancellationToken cancellationToken)
    {
        if (!_canStartListenExecute) return;
        _canStartListenExecute = true;

        await _speechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), cancellationToken);
        _speechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }
    public async Task StopListen(CancellationToken cancellationToken)
    {
        _canStartListenExecute = true;

        _speechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        await _speechToText.StopListenAsync(cancellationToken);
    }

    //Helpers
    private void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        var text = e.RecognitionResult;
    }
}
