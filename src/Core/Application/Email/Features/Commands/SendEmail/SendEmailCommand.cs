using Application.Email.Dtos;

namespace Application.Email.Features.Commands.SendEmail;

public class SendEmailCommand(EmailDto emailModel) : IRequest<Result<int>>
{
    internal EmailDto EmailMessage { get; } = emailModel;
}
