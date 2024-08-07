using Application.Email.Dtos;

namespace Application.Email.Abstractions.Services;

public interface IEmailSender
{
    //Properties
    bool IsConnected { get; }

    //Methods
    Task<Result> SendEmailAsync(EmailDto message, CancellationToken token = default);
    Task<Result> ConnectAsync(string emailAddress, string password, CancellationToken token = default);
}
