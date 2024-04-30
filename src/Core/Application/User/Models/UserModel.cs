using Domain.User.Entities;

namespace Application.User.Models;

public class UserModel : Model
{
    //Properties
    public string UserName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string EmailPassword { get; set; } = string.Empty;
    public string EmailDomain => this.EmailAddress[(this.EmailAddress.LastIndexOf('@') + 1)..] ?? string.Empty;

    //Methods
    public static UserModel CreateFromEntity(UserEntity userEntity)
    {
        return new UserModel()
        {
            EntityId = userEntity.Id,
            CreatedAt = userEntity.CreatedAt,
            ModifiedAt = userEntity.ModifiedAt,
            UserName = userEntity.UserName,
            EmailAddress = userEntity.EmailAddress.Value,
            EmailPassword = userEntity.EmailPassword.Value
        };
    }
}