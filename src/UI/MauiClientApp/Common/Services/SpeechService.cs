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
        try
        {
            if (token.IsCancellationRequested)
            {
                return Result.Failure(new Error("SpeecToText.SpeakAsync.Cancelled", ""));
            }

            OnSpeechAnounced?.Invoke(text);
            await TextToSpeech.SpeakAsync(text, new SpeechOptions()
            {
                Pitch = 1,
                Volume = 1,
            }, token);
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("SpeecToText.SpeakAsync.Failed", ex.Message));
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
        if (token.IsCancellationRequested)
        {
            return Result.Failure<string>(new Error("SpeecToText.ListenAsync.Cancelled", ""));
        }

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
                return Result.Failure<string>(new Error("SpeecToText.ListenAsync.Failed", ""));
            }

            return Result.Success(recognitionText); 
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(new Error("SpeecToText.ListenAsync.Failed", ex.Message));
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
