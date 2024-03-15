using Domain.Email.Entities;
using Domain.Email.Entities.Enums;

namespace Domain.UnitTests.Email.EntityTests;

[TestFixture]
public class EmailEntityTests
{
    [Test]
    public void Create_ThrowsArgumentNullException_ForNullRecipients()
    {
        Assert.Throws<ArgumentNullException>(() => EmailEntity.Create(null, EmailSubjectLine.Create("Subject"), EmailBodyText.Create("Body")));
    }

    [Test]
    public void Create_ThrowsArgumentNullException_ForNullSubject()
    {
        Assert.Throws<ArgumentNullException>(() => EmailEntity.Create([EmailAddress.Create("test@example.com")], null, EmailBodyText.Create("Body")));
    }

    [Test]
    public void Create_ThrowsArgumentNullException_ForNullBody()
    {
        Assert.Throws<ArgumentNullException>(() => EmailEntity.Create([EmailAddress.Create("test@example.com")], EmailSubjectLine.Create("Subject"), null));
    }

    [Test]
    public void Create_SetsProperties_ForValidArguments()
    {
        //Arrange
        var recipients = new List<EmailAddress>() { EmailAddress.Create("test@example.com") };
        var subject = EmailSubjectLine.Create("This is a subject");
        var body = EmailBodyText.Create("This is the email body");

        //Act
        var email = EmailEntity.Create(recipients, subject, body);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(email.Recipients, Is.EqualTo(recipients));
            Assert.That(email.Subject, Is.EqualTo(subject));
            Assert.That(email.Body, Is.EqualTo(body));
            Assert.That(email.Type, Is.EqualTo(EmailType.Email));
            Assert.That(email.EmailStatus, Is.EqualTo(EmailStatus.UnRead));
        });
    }

    [Test]
    public void Create_AddEmailEntityCreatedEvent_ForValidArguments()
    {
        //Arrange
        var recipients = new List<EmailAddress>() { EmailAddress.Create("test@example.com") };
        var subject = EmailSubjectLine.Create("This is a subject");
        var body = EmailBodyText.Create("This is the email body");

        //Act
        var email = EmailEntity.Create(recipients, subject, body);

        //Assert
        Assert.That(email.DomainEvents, Has.Count.EqualTo(1));
    }
}

