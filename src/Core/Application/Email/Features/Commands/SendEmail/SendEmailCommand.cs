namespace Application.Email.Features.Commands.SendEmail;

public class SendEmailCommand(EmailModel emailModel) : IRequest<Result<int>>
{
    public EmailModel EmailMessage { get; } = emailModel;
}
