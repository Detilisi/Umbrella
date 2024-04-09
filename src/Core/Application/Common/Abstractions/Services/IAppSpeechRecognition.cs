namespace Application.Common.Abstractions.Services;

public interface IAppSpeechRecognition : IAsyncDisposable
{
    Task StopListenAsync(CancellationToken cancellationToken = default);
    Task<string> ListenAsync(CancellationToken cancellationToken = default);
    Task<bool> RequestPermissions(CancellationToken cancellationToken = default);
}
