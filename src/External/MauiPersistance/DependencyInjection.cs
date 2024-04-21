using Application.Common.Abstractions.DataContexts;
using MauiPersistence.Common.DataContexts;

namespace MauiPersistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string connectionString)
    {
        //AddDbContext
        services.AddSingleton<IApplicationDbContext>((provider) =>
        {
            return new ApplicationDbContext(connectionString);
        });
        
        return services;
    }
}
