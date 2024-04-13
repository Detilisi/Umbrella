using Application.User.Models;

namespace Application.User.Features.Queries.LoginUser;

public class LoginUserQuery(string emailAddress, string emailPassword) : IRequest<UserModel>
{
    public string EmailAddress { get; set; } = emailAddress;
    public string EmailPassword { get; set; } = emailPassword;
}
