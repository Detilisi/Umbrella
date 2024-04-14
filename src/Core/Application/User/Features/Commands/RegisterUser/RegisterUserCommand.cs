using Application.User.Models;

namespace Application.User.Features.Commands.RegisterUser;

public class RegisterUserCommand(UserModel user) : IRequest<Result<int>>
{
    public UserModel User { get; set; } = user;
}
