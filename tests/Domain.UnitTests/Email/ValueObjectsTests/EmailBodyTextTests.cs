namespace Domain.UnitTests.Email.ValueObjectsTests;

[TestFixture]
public class EmailBodyTextTests
{
    [Test]
    public void Create_ThrowsEmptyValueException_ForEmptyString()
    {
        Assert.Throws<EmptyValueException>(() => EmailBodyText.Create(""));
    }

    [Test]
    public void Create_ThrowsEmptyValueException_ForWhitespaceString()
    {
        Assert.Throws<EmptyValueException>(() => EmailBodyText.Create(" "));
    }

    [Test]
    public void Create_ThrowsEmailBodyTooLongException_ForExceedingMaxBodyLength()
    {
        var excessivelyLongText = new string('x', EmailBodyText.MAXBODYLENGTH + 1);
        Assert.Throws<EmailBodyTooLongException>(() => EmailBodyText.Create(excessivelyLongText));
    }

    [Test]
    public void Create_SetsText_ForValidBody()
    {
        var bodyText = "This is the email body content.";
        var emailBody = EmailBodyText.Create(bodyText);
        Assert.That(emailBody.Value, Is.EqualTo(bodyText));
    }

    [Test]
    public void ToString_ReturnsText()
    {
        var bodyText = "This is the email body content.";
        var emailBody = EmailBodyText.Create(bodyText);
        Assert.That(emailBody.ToString(), Is.EqualTo(bodyText));
    }
}
