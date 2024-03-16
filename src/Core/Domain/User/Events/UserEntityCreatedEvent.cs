using Domain.User.Entities;

namespace Domain.User.Events;

public class UserEntityCreatedEvent(UserEntity userEntity) : Event
{
    public UserEntity EventEntity { get; set; } = userEntity;
}
