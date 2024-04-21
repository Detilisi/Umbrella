using Application.Common.Abstractions.DataContexts;
using MauiPersistence.Common.DataContexts;

namespace MauiPersistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
    {
        //AddDbContext
        /*services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlite(connectionString,
               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));*/

        services.AddScoped<IApplicationDbContext>((provider) =>
        {
            return new ApplicationDbContext(connectionString);
        });

        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}
