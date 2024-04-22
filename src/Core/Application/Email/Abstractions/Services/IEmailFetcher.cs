namespace Application.Email.Abstractions.Services;

public interface IEmailFetcher : IDisposable
{
    //Properties
    bool IsConnected { get; }
    
    //Methods
    Task<Result<List<EmailModel>>> LoadEmailsAsync(CancellationToken token = default);
    Task<Result> ConnectAsync(UserModel userModel, CancellationToken token = default);
}
