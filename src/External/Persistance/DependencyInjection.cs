using Application.Common.Abstractions.DataContexts;
using Persistence.Common.Configs;
using Persistence.Common.DataContexts;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services)
    {
        //AddDbContext
        services.AddSingleton<IApplicationDbContext>((provider) =>
        {
            return new ApplicationDbContext(UmbrellaSqliteConfigs.DatabasePath);
        });
        
        return services;
    }
}
