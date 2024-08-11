namespace Application.Common.Abstractions.DataContexts;

public interface IApplicationDbContext
{
    //Data sets
    DbSet<UserEntity> Users { get; }
    DbSet<EmailEntity> Emails { get; }
    DbSet<ContactEntity> Conctacts { get; }

    //Methods
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
