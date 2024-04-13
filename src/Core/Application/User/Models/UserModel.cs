using Domain.User.ValueObjects;

namespace Application.User.Models;

public class UserModel : Model
{
    public string UserName { get; set; } = string.Empty;
    public required EmailAddress EmailAddress { get; set; }
    public required EmailPassword EmailPassword { get; set; }
}