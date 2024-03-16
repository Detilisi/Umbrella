using Domain.User.Events;
using Domain.User.ValueObjects;

namespace Domain.User.Entities;

public class UserEntity : Entity
{
    //Properties
    public EmailAddress EmailAddress { get; private set; }
    public EmailPassword EmailPassword { get; private set; }
    public string UserName { get; private set; } = string.Empty;

    //Construction
    private UserEntity
    (
        int id, 
        EmailAddress emailAddress, 
        EmailPassword password, 
        string username
    ) : base(id)
    {
        UserName = username;
        EmailPassword = password;
        EmailAddress = emailAddress;

        AddDomainEvent(new UserEntityCreatedEvent(this));
    }

    public static UserEntity Create
    (
        EmailAddress emailAddress, 
        EmailPassword password, 
        string username
    )
    {
        return new UserEntity(0, emailAddress, password, username);
    }
}
