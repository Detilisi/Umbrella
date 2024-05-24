﻿using Infrastructure.Email.Services;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailFetcher, EmailFetcher>();

        return services;
    }
}
