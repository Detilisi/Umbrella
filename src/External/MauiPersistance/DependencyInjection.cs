using Application.Common.Abstractions.DataContexts;
using MauiPersistence.Common.DataContexts;

namespace MauiPersistence;

public static class DependencyInjection
{
    public static void AddPersistenceLayer(this IServiceCollection services, string connectionString)
    {
        //AddDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlite(connectionString,
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
}
