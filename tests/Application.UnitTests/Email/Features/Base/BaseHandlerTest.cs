namespace Application.UnitTests.Email.Features.Base;

public class BaseHandlerTest
{
    protected Mock<IApplicationDbContext> MockDbContext { set; get; } = null!;
    protected Mock<DbSet<UserEntity>> MockUserEntitySet { set; get; } = null!;
    protected Mock<DbSet<EmailEntity>> MockEmailEntitySet { set; get; } = null!;

    [SetUp]
    public void Setup()
    {
        
        MockDbContext = new Mock<IApplicationDbContext>();
        MockUserEntitySet = new Mock<DbSet<UserEntity>>();
        MockEmailEntitySet = new Mock<DbSet<EmailEntity>>();
        
        MockDbContext.Setup(c => c.Users).Returns(MockUserEntitySet.Object);
        MockDbContext.Setup(c => c.Emails).Returns(MockEmailEntitySet.Object);
        MockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
    }
}
