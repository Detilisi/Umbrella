namespace Domain.UnitTests.Common.ValueObjectsTests;

[TestFixture]
public class EmailAddressTests
{
    [Test]
    public void Create_ThrowsEmptyValueException_ForEmptyString()
    {
        Assert.Throws<EmptyValueException>(() => EmailAddress.Create(""));
    }

    [Test]
    public void Create_ThrowsEmptyValueException_ForWhitespaceString()
    {
        Assert.Throws<EmptyValueException>(() => EmailAddress.Create(" "));
    }

    [TestCase("invalid-email")]
    [TestCase("missing@dotcom")]
    public void Create_ThrowsInvalidEmailAddressException_ForInvalidEmail(string invalidEmail)
    {
        Assert.Throws<InvalidEmailAddressException>(() => EmailAddress.Create(invalidEmail));
    }

    [Test]
    public void Create_SetsAddress_ForValidEmail()
    {
        var email = EmailAddress.Create("test@example.com");
        Assert.That(email.Value, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void Create_SetsDomain_ForValidEmail()
    {
        var email = EmailAddress.Create("test@example.com");
        Assert.That(email.Domain, Is.EqualTo("example.com"));
    }
}

