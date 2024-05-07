using SkiaSharp.Extended.UI.Controls;

namespace MauiClientApp.Email.EmailSync.Views;

public class EmailSyncIndicatorView : ContentView
{
    //View components
    private Label HeaderLabel = null!;
    private Label WaiterLabel = null!;
    private SKLottieView SyncLottieView = null!;

    //Construction
    public EmailSyncIndicatorView()
    {
        InitializeLabels();
        InitializeLottieView();

        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                SyncLottieView,
                HeaderLabel, 
                WaiterLabel,
            }
        };
    }

    //View component Initialization
    private void InitializeLabels()
    {
        HeaderLabel = new Label()
        {
            FontSize = 22,
            Text = "Syncing emails",
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center,
        };

        WaiterLabel = new Label()
        {
            MaxLines = 1,
            FontSize = 18,
            Text = "Please wait...",
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center,
        };
    }

    private void InitializeLottieView()
    {
        SyncLottieView = new SKLottieView()
        {
            RepeatCount = -1,
            WidthRequest = 200,
            HeightRequest = 200,
            BackgroundColor = Colors.White,
            Source = new SKFileLottieImageSource()
            {
                File = "AnikiHamster.json"
            }
        };
    }

}
