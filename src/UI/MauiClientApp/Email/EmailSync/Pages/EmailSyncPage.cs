using MauiClientApp.Email.EmailSync.ViewModels;
using SkiaSharp.Extended.UI.Controls;

namespace MauiClientApp.Email.EmailSync.Pages;

public class EmailSyncPage(EmailSyncViewModel viewModel) : EmailPage<EmailSyncViewModel>(viewModel, new VerticalStackLayout())
{
    protected override ScrollView PageContent => new()
    {
        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            Padding = 30,
            VerticalOptions = LayoutOptions.Center,

            Children =
            {
                new Label()
                {
                    Text = "Syncing emails",
                    MaxLines = 1,
                    FontSize = 28,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                },
                new SKLottieView()
                {
                    Source = new SKFileLottieImageSource()
                    {
                        File = "AnikiHamster.json"
                    },
                    RepeatCount = -1,
                    WidthRequest = 200,
                    HeightRequest = 200,
                    BackgroundColor = Colors.AliceBlue,
                    HorizontalOptions = LayoutOptions.Center
                },
                new Label()
                { 
                    Text = "Please wait...",
                    MaxLines = 1,
                    FontSize = 28,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                },
            }
        }
    };
}
