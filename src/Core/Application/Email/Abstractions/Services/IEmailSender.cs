namespace Application.Email.Abstractions.Services;

public interface IEmailSender
{
    Task<Result> SendEmailAsync(EmailModel message);
}
