using System.Reflection;

namespace MauiPersistance.Common.DataContexts;

public class ApplicationDbContext : DbContext
{
    //Fields
    public static string File { get; protected set; }
    public static bool Initialized { get; protected set; }

    //Construction
    public ApplicationDbContext() 
    {
        File = Path.Combine("../", "UsedByMigratorOnly1.db3");
        Initialize();
    }
    public ApplicationDbContext(string filenameWithPath)
    {
        File = filenameWithPath;
        Initialize();
    }

    //Data sets
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<EmailEntity> Emails => Set<EmailEntity>();

    //Configurations
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlite($"Filename={File}");
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    //Helper method
    private void Initialize()
    {
        if (!Initialized)
        {
            Initialized = true;

            SQLitePCL.Batteries_V2.Init();

            Database.Migrate();
        }
    }
}
