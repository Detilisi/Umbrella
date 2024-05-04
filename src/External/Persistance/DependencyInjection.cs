using Application.Common.Abstractions.DataContexts;
using Persistence.Common.DataContexts;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, string dbName)
    {
        //AddDbContext
        services.AddSingleton<IApplicationDbContext>((provider) =>
        {
            return new ApplicationDbContext(dbName);
        });
        
        return services;
    }
}
