using Domain.User.Entities;
using Domain.User.ValueObjects;

namespace Domain.UnitTests.User.EntityTests;

public class UserEntityTests
{
    [Test]
    public void Create_ThrowsArgumentNullException_ForNullEmailAddress()
    {
        Assert.Throws<ArgumentNullException>(() => UserEntity.Create(null, EmailPassword.Create("password"), "username"));
    }

    [Test]
    public void Create_ThrowsArgumentNullException_ForNullPassword()
    {
        Assert.Throws<ArgumentNullException>(() => UserEntity.Create(EmailAddress.Create("test@example.com"), null, "username"));
    }

    [Test]
    public void Create_SetsProperties_ForValidArguments()
    {
        //Arrange
        var emailAddress = EmailAddress.Create("test@example.com");
        var password = EmailPassword.Create("ThisIsAValidPassword123");
        var username = "JohnDoe";

        //Act
        var user = UserEntity.Create(emailAddress, password, username);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
            Assert.That(user.EmailPassword, Is.EqualTo(password));
            Assert.That(user.UserName, Is.EqualTo(username));
        });
    }

    [Test]
    public void Create_AddEmailEntityCreatedEvent_ForValidArguments()
    {
        //Arrange
        var emailAddress = EmailAddress.Create("test@example.com");
        var password = EmailPassword.Create("ThisIsAValidPassword123");
        var username = "JohnDoe";

        //Act
        var user = UserEntity.Create(emailAddress, password, username);

        //Assert
        Assert.That(user.DomainEvents, Has.Count.EqualTo(1));
    }
}
