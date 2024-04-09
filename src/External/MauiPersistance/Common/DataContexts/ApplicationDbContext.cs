using System.Reflection;
using Application.Common.Abstractions.DataContexts;

namespace MauiPersistence.Common.DataContexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    //Fields
    private bool _initialized;
    private readonly string _connectionString;

    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Construction
    public ApplicationDbContext()
    {
        _connectionString = Path.Combine("../", "MigratorExclusive.db3");
        InitializeDataBase();
    }
    public ApplicationDbContext(string dbPath)
    {
        _connectionString = dbPath;
        InitializeDataBase();
    }

    //Configurations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Filename={_connectionString}");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    //Helper method
    private void InitializeDataBase()
    {
        if (!_initialized) return;

        SQLitePCL.Batteries_V2.Init();
        Database.Migrate();
        _initialized = true;
    }
}
