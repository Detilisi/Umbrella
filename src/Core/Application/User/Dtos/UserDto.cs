using Domain.User.Entities;

namespace Application.User.Dtos;

public class UserDto : Dto
{
    //Properties
    public string EncrytedPassword { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public required string EmailAddress { get; set; } = string.Empty;

    //Methods
    internal static UserDto CreateFromEntity(UserEntity userEntity)
    {
        return new UserDto()
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