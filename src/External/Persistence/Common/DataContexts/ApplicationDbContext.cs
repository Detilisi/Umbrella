using System.Reflection;

namespace Persistence.Common.DataContexts;

public class ApplicationDbContext : DbContext
{
    //Fields
    private readonly IMediator _mediator;

    //Construction
    public ApplicationDbContext(){}

    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Configurations
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
