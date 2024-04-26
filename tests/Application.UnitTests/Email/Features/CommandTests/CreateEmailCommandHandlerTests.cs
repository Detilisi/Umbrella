using Application.Email.Features.Commands.CreateEmail;
using Application.UnitTests.Email.Features.Base;
using Domain.Common.Exceptions;

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

    [Test]
    public void Handle_Should_ThrowArgumentNullException_When_Sender_IsNull()
    {
        var handler = new CreateEmailCommandHandler(MockDbContext.Object);
        var command = new CreateEmailCommand
        {
            Sender = null,
            Recipients = ["recipient1@example.com", "recipient2@example.com"],
            Subject = "Test Subject",
            Body = "Test Body"
        };

        //Act & Assert
        Assert.ThrowsAsync<EmptyValueException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}