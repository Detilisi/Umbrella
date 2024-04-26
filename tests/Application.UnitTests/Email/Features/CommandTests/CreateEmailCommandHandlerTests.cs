using Application.Email.Features.Commands.CreateEmail;
using Application.UnitTests.Email.Features.Base;

namespace Application.UnitTests.Email.Features.Commands;

[TestFixture]
public class CreateEmailCommandHandlerTests : BaseHandlerTest
{
    [Test]
    public async Task Handle_Should_CreateEmail_When_CommandIsValid()
    {
        // Arrange
        var handler = new CreateEmailCommandHandler(MockDbContext.Object);
        var command = new CreateEmailCommand
        {
            Sender = "sender@example.com",
            Recipients = ["recipient1@example.com", "recipient2@example.com"],
            Subject = "Test Subject",
            Body = "Test Body"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        MockDbContext.Verify(db => db.Emails.Add(It.IsAny<EmailEntity>()), Times.Once);
        MockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}