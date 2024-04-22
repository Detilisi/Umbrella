namespace Infrastructure.Common.Services;

public class AppTextToSpeech : IAppTextToSpeech
{
    //Fields
    private readonly ITextToSpeech _textToSpeech;
    private readonly SpeechOptions _speechOptions;

    //Properties
    public float Pitch { get; private set; }
    public float Volume { get; private set; }

    //Construction
    public AppTextToSpeech()
    {
        _textToSpeech = TextToSpeech.Default;
        _speechOptions = new SpeechOptions()
        {
            Pitch = 1.0f,
            Volume = 1.0f,
        };
    }

    //Options method
    public void SetSpeechOptions(float pitch, float volume)
    {
        _speechOptions.Volume = Pitch = pitch;
        _speechOptions.Volume = Volume = volume;
    }

    //Speak method
    public async Task SpeakAsync(string text, CancellationToken cancelToken = default)
    {
        await _textToSpeech.SpeakAsync(text, _speechOptions, cancelToken);
    }
}
