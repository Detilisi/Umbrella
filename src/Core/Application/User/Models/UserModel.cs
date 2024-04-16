using Domain.User.Entities;

namespace Application.User.Models;

public class UserModel : Model
{
    public string UserName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string EmailPassword { get; set; } = string.Empty;
    public string EmailDomain { get; set; } = string.Empty;

    public static UserModel CreateFromUserEntity(UserEntity userEntity)
    {
        return new UserModel()
        {
            EntityId = userEntity.Id,
            EmailAddress = userEntity.EmailAddress.Address,
            EmailPassword = userEntity.EmailPassword.Key,
            EmailDomain = userEntity.EmailAddress.Domain,
            CreatedAt = userEntity.CreatedAt,
            ModifiedAt = userEntity.ModifiedAt,
            UserName = userEntity.UserName,
        };
    }
}