using Application.User.Dtos;

namespace Application.User.Features.Commands.RegisterUser;

public class RegisterUserCommand : UserDto, IRequest<Result<int>>
{
}
