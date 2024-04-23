namespace Application.Email.Abstractions.Services;

public interface IEmailSender
{
    //Properties
    bool IsConnected { get; }

    //Methods
    Task<Result> SendEmailAsync(EmailModel message, CancellationToken token = default);
    Task<Result> ConnectAsync(UserModel userModel, CancellationToken token = default);
}
