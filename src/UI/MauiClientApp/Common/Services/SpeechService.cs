using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using System.Globalization;

namespace MauiClientApp.Common.Services;

internal static class SpeechService
{
    //Fields
    const string defaultLanguage = "en-US";
    private static bool CanListenExecute { get; set; }
    private static bool CanStartListenExecute { get; set; }
    private static ITextToSpeech TextToSpeech { get; } = Microsoft.Maui.Media.TextToSpeech.Default;
    private static ISpeechToText SpeechToText { get; } = CommunityToolkit.Maui.Media.SpeechToText.Default;

    //Propertues
    internal static Action<string> OnSpeechAnounced { get; set; } = null!;
    internal static Action<string> OnSpeechRecognized { get; set; } = null!;

    //Permision method
    internal static void RequestPermissions(CancellationToken token = default)
    {
        var isGranted = SpeechToText.RequestPermissions(token);
        CanListenExecute = isGranted.Result;
        CanStartListenExecute = isGranted.Result;
    }

    //Speak methods
    internal static async Task<Result> SpeakAsync(string text, CancellationToken token = default)
    {
        var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            OnSpeechAnounced?.Invoke(text);

            var options = new SpeechOptions()
            {
                Pitch = 1,
                Volume = 1,
            };
            await TextToSpeech.SpeakAsync(text, options, token);
            
            //.WaitAsync(timeoutCancellationTokenSource.Token);

            return Result.Success();
        }
        catch (TaskCanceledException)
        {
            return Result.Failure(new Error("SpeecToText.Speak Failed", ""));
        }
    }

    //Listen methods
    internal static async Task StartListenAsync(CancellationToken token = default)
    {
        if (!CanStartListenExecute) return;
        CanStartListenExecute = false;

        await SpeechToText.StartListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), token);
        SpeechToText.RecognitionResultUpdated += HandleRecognitionResultUpdated;
    }
    internal static async Task<Result<string>> ListenAsync(CancellationToken token = default)
    {
        RequestPermissions(token);
        CanListenExecute = false;
        var recognitionText = string.Empty;

        try
        {
            var recognitionResult = await SpeechToText.ListenAsync
            (
                CultureInfo.GetCultureInfo(defaultLanguage), 
                new Progress<string>(), 
                token
            );

            if (recognitionResult.IsSuccessful)
            {
                recognitionText = recognitionResult.Text;
            }
            else
            {
                recognitionText = "Error";
                return Result.Failure<string>(new Error("SpeecToText.Listen Failed", ""));
            }


            return Result.Success(recognitionText); 
        }
        finally
        {
            CanListenExecute = true;
            OnSpeechRecognized?.Invoke(recognitionText);
        }
    }
    internal static async Task StopListenAsync(CancellationToken token = default)
    {
        if (CanStartListenExecute || CanListenExecute) return;
        CanListenExecute = true;
        CanStartListenExecute = true;

        SpeechToText.RecognitionResultUpdated -= HandleRecognitionResultUpdated;

        await SpeechToText.StopListenAsync(token);
    }

    //Helpers
    private static void HandleRecognitionResultUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs e)
    {
        var text = e.RecognitionResult;
    }
}
