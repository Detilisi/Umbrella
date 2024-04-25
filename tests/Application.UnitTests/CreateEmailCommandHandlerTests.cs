using Application.Common.Abstractions.DataContexts;
using Application.Email.Features.Commands.CreateEmail;
using Domain.Email.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests;
public class CreateEmailCommandHandlerTests
{
    private Mock<IApplicationDbContext> _dbContextMock;

    [SetUp]
    public void Setup()
    {
        var mockSet = new Mock<DbSet<EmailEntity>>();
        _dbContextMock = new Mock<IApplicationDbContext>();
        _dbContextMock.Setup(c => c.Emails).Returns(mockSet.Object);
    }

    [Test]
    public async Task Handle_SavesEmailAndReturnsId_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateEmailCommand
        {
            EntityId = 1,
            Sender = "sender@example.com",
            Recipients = ["recipient@example.com"],
            Subject = "Test Email Subject",
            Body = "This is a test email body"
        };

        _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        var handler = new CreateEmailCommandHandler(_dbContextMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        _dbContextMock.Verify(db => db.Emails.Add(It.IsAny<EmailEntity>()), Times.Once);
        _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.EqualTo(1));
        });
    }
}
