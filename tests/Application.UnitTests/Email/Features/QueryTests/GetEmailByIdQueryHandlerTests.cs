using Application.Email.Features.Queries.GetEmailById;
using Application.UnitTests.Email.Features.Base;
using Domain.Common.ValueObjects;
using Domain.Email.ValueObjects;

namespace Application.UnitTests.Email.Features.QueryTests;

public class GetEmailByIdQueryHandlerTests : BaseHandlerTest
{
    [SetUp]
    public new void Setup()
    {
        base.Setup();

        //Act
        var testEmailEntity = EmailEntity.Create
        (
            EmailAddress.Create("test@example.com"), 
            [EmailAddress.Create("test@example.com"), EmailAddress.Create("test@example.com")],
            EmailSubjectLine.Create("This is a subject"),
            EmailBodyText.Create("This is the email body")
        );

        MockDbContext.Setup(db => db.Emails.Where(x => x.Id == It.IsAny<int>()))
            .Returns(new List<EmailEntity> { testEmailEntity }.AsQueryable());
        MockDbContext.Setup(db => db.Emails.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(testEmailEntity));
    }

    [Test]
    public async Task Handle_Should_ReturnEmailModel_When_EmailExists()
    {
        // Arrange
        var testEmailId = 1;
        var query = new GetEmailByIdQuery(testEmailId, testEmailId);
        var handler = new GetEmailByIdQueryHandler(MockDbContext.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.IsSuccess);
        Assert.Equals(testEmailId, result.Value.EntityId);

        MockDbContext.Verify(db => db.Emails.Where(x => x.Id == testEmailId), Times.Once);
        MockDbContext.Verify(db => db.Emails.FirstOrDefaultAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

}
