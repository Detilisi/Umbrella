using Application.Email.Dtos;

namespace Application.Email.Abstractions.Services;

public interface IEmailFetcher : IDisposable
{
    //Properties
    bool IsConnected { get; }
    
    //Methods
    Task<Result<List<EmailDto>>> LoadEmailsAsync(CancellationToken token = default);
    Task<Result<List<EmailDto>>> LazyLoadEmailsAsync(int pageSize, int skip, CancellationToken token = default);
    Task<Result> ConnectAsync(string emailAddress, string password, CancellationToken token = default);
}
