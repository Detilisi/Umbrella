using MauiPersistence.Common.DataContexts;
using Microsoft.Extensions.DependencyInjection;

namespace MauiPersistence.Extensions;

public static class DependencyInjection
{
    public static void AddPersistenceLayer(this IServiceCollection services, string connectionString)
        //AddDbContext
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlite(connectionString,
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    }
}
