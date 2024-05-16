using Infrastructure;
using Application;
using Persistence;
using Microsoft.Extensions.Logging;
using MauiClientApp.Email.EmailList.ViewModels;
using MauiClientApp.Email.EmailList.Pages;
using MauiClientApp.Email.EmailSync.Pages;
using MauiClientApp.Email.EmailSync.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;
using MauiClientApp.Email.EmailDetail.Pages;
using MauiClientApp.Email.EmailDetail.ViewModels;
using CommunityToolkit.Maui;

namespace MauiClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeSolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //Register Layers
            builder.Services.AddApplicationLayer();
            builder.Services.AddPersistenceLayer();
            builder.Services.AddInfrastructureLayer();
            
            //Register ViewViewModels + Pages
            builder.Services.AddTransientWithShellRoute<EmailSyncPage, EmailSyncViewModel>(nameof(EmailSyncPage));
            builder.Services.AddTransientWithShellRoute<EmailListPage, EmailListViewModel>(nameof(EmailListPage));
            builder.Services.AddTransientWithShellRoute<EmailDetailPage, EmailDetailViewModel>(nameof(EmailDetailPage));

            return builder.Build();
        }
    }

    
}
