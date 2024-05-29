namespace Application.Email.Features.Commands.SendEmail;

public class SendEmailCommand(EmailModel emailModel) : IRequest<Result<int>>
{
    internal EmailModel EmailMessage { get; } = emailModel;
}
