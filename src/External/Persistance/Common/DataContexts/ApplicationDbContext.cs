using Application.Common.Abstractions.DataContexts;
using System.Reflection;

namespace Persistence.Common.DataContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    //Fields
    private bool _initialized = false;
    private readonly string _connectionString;

    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();
    public DbSet<ContactEntity> Conctacts => Set<ContactEntity>();

    //Construction
    public ApplicationDbContext()
    {
        _connectionString = Path.Combine("../", "migrator.db3");
        InitializeDataBase();
    }
    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDataBase();
    }

    //Configurations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={_connectionString}",
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    //Helper method
    private void InitializeDataBase()
    {
        if (_initialized) return;

        SQLitePCL.Batteries_V2.Init();
        Database.Migrate();
        _initialized = true;
    }
}
