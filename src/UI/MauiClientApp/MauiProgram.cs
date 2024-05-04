using Infrastructure;
using Application;
using Persistence;
using Microsoft.Extensions.Logging;
using MauiClientApp.Email.EmailList.ViewModels;
using MauiClientApp.Email.EmailList.Pages;

namespace MauiClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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

            var connectionString = Path.Combine(FileSystem.AppDataDirectory, "umbrella.db3"); //Place in app config file

            //Register Layers
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructureLayer();
            builder.Services.AddPersistenceLayer(connectionString);

            //Register Views
            builder.Services.AddTransient<EmailListPage, EmailListViewModel>();

            return builder.Build();
        }
    }
}
