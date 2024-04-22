using CommunityToolkit.Maui.Media;
using Shared.Common.Results;
using System.Globalization;

namespace Infrastructure.Common.Services;

public class AppSpeechRecognition : IAppSpeechRecognition
{
    //Fields
    private readonly ISpeechToText _speechToText;
    private const string _defaultLanguage = "en-US";

    //Construction
    public AppSpeechRecognition()
    {
        _speechToText = SpeechToText.Default;
    }

    //Permision method
    public Task<bool> RequestPermissions(CancellationToken cancellationToken = default)
    {
        return _speechToText.RequestPermissions(cancellationToken);
    }

    //Listen method
    public async Task<Result<string>> ListenAsync(CancellationToken cancellationToken = default)
    {
        var recognitionResult = await _speechToText.ListenAsync(
            CultureInfo.GetCultureInfo(_defaultLanguage), new Progress<string>(), cancellationToken
        );

        if (recognitionResult.IsSuccessful)
        {
            return Result.Success(recognitionResult.Text);
        }

        return Result.Failure<string>(new Error("SpeechRecognitionFailed", "Didn't catch that, please try again"));
    }

    //Stop method
    public Task StopListenAsync(CancellationToken cancellationToken = default)
    {
        return _speechToText.StopListenAsync(cancellationToken);
    }

    //Dispose method
    public ValueTask DisposeAsync() => _speechToText.DisposeAsync();
}
