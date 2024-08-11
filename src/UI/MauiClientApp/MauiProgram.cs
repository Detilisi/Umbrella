using Application;
using Infrastructure;
using Microsoft.Extensions.Logging;
using Persistence;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
            builder.Services.AddTransientWithShellRoute<EmailEditPage, EmailEditViewModel>(nameof(EmailEditPage));
            builder.Services.AddTransientWithShellRoute<SignUpPage, SignUpViewModel>(nameof(SignUpPage));
            builder.Services.AddTransientWithShellRoute<ContactsListPage, ContactsListViewModel>(nameof(ContactsListPage));

            return builder.Build();
        }
    }

    
}
