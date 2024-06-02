using Domain.User.Entities;

namespace Application.User.Models;

public class UserModel : Model
{
    //Properties
    public string EncrytedPassword { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public required string EmailAddress { get; set; } = string.Empty;
    
    //Methods
    internal static UserModel CreateFromEntity(UserEntity userEntity)
    {
        return new UserModel()
        {
            EntityId = userEntity.Id,
            CreatedAt = userEntity.CreatedAt,
            ModifiedAt = userEntity.ModifiedAt,
            UserName = userEntity.UserName,
            EmailAddress = userEntity.EmailAddress.Value,
            EncrytedPassword = userEntity.EmailPassword.Value
        };
    }
}