namespace Application.User.Features.Commands.RegisterUser;

public class RegisterUserCommand(UserModel userModel) :IRequest<Result<int>>
{
    internal UserModel User { get; } = userModel;
}
