using SkiaSharp.Extended.UI.Controls;

namespace MauiClientApp.Email.EmailSync.Views;

public class EmailSyncIndicatorView : ContentView
{
    //Construction
    public EmailSyncIndicatorView()
    {
        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new SKLottieView()
                {
                    RepeatCount = -1,
                    WidthRequest = 300,
                    HeightRequest = 300,
                    Source = new SKFileLottieImageSource()
                    {
                        File = "EmailSync.json"
                    }
                },
                new Label()
                {
                    FontSize = 22,
                    Text = "Syncing emails",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                },
                new Label()
                {
                    MaxLines = 1,
                    FontSize = 18,
                    Text = "Please wait...",
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                }
            }
        };
    }
}
