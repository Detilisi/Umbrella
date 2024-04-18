using Domain.User.Exceptions;
using Domain.User.ValueObjects;

namespace Domain.UnitTests.User.ValueObjectsTests;

[TestFixture]
public class EmailPasswordTests
{
    [Test]
    public void Create_ThrowsEmptyValueException_ForEmptyString()
    {
        Assert.Throws<EmptyValueException>(() => EmailPassword.Create(""));
    }

    [Test]
    public void Create_ThrowsEmptyValueException_ForWhitespaceString()
    {
        Assert.Throws<EmptyValueException>(() => EmailPassword.Create(" "));
    }

    [Test]
    public void Create_ThrowsPasswordTooShortException_ForPasswordLessThanMinimumLength()
    {
        var shortPassword = new string('x', EmailPassword.MINIMUMPASSWORDLENGTH - 1);
        Assert.Throws<PasswordTooShortException>(() => EmailPassword.Create(shortPassword));
    }

    [Test]
    public void Create_SetsValue_ForValidPassword()
    {
        var password = "ThisIsAValidPassword123";
        var emailPassword = EmailPassword.Create(password);
        Assert.That(emailPassword.Value, Is.EqualTo(password));
    }

    [Test]
    public void ToString_ReturnsValue()
    {
        var password = "ThisIsAValidPassword123";
        var emailPassword = EmailPassword.Create(password);
        Assert.That(emailPassword.ToString(), Is.EqualTo(password));
    }
}
