namespace Application.Common.Abstractions.Services;

public interface IAppTextToSpeech
{
    //Properties
    public float Pitch { get; }
    public float Volume { get; }

    //Methods
    void SetSpeechOptions(float pitch, float volume);
    Task SpeakAsync(string text, CancellationToken cancelToken = default);
}
