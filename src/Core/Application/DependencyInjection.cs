using System.Reflection;
using MauiClientApp.Email.EmailList.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddSingleton<EmailListViewModel>();

        return services;
    }
}
