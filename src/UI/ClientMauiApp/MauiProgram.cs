using Infrastructure;
using Microsoft.Extensions.Logging;

namespace ClientMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
            });

        // Initialise the toolkit
        builder.UseMauiApp<App>().UseMauiCommunityToolkitMarkup();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var connectionString = Path.Combine(FileSystem.AppDataDirectory, "umbrella.db3");

        builder.Services.AddPersistenceLayer(connectionString);
        builder.Services.AddInfrastructureLayer();
        builder.Services.AddApplicationLayer();

        builder.Services.AddSingleton<MainPage>();
        
        return builder.Build();
    }
}
