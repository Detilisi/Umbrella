namespace Application.Email.Abstractions.Services;

public interface IEmailFetcher : IDisposable
{
    //Properties
    bool IsConnected { get; }
    
    //Methods
    //Task<List<EmailModel>> LoadEmailsAsync(CancellationToken token = default);
    //Task ConnectAsync(UserModel userModel, CancellationToken token = default);
}
