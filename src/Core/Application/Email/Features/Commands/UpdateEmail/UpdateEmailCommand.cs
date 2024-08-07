using Application.Email.Dtos;

namespace Application.Email.Features.Commands.UpdateEmail;

public class UpdateEmailCommand : EmailDto, IRequest<Result<int>>
{
}
