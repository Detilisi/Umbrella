namespace Application.User.Features.Queries.LoginUser;

public class LoginUserQuery(string emailAddress, string emailPassword) : IRequest<Result<UserModel>>
{
    public string EmailAddress { get; set; } = emailAddress;
    public string EmailPassword { get; set; } = emailPassword;
}
