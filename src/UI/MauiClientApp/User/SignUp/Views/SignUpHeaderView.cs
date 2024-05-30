namespace MauiClientApp.User.SignUp.Views;

internal class SignUpHeaderView : ContentView
{
    //View components
    private Label HeaderLabel = null!;
    private Frame HeaderFrameImage = null!;

    //Construction
    public SignUpHeaderView()
    {
        InitializeComponents();

        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                HeaderFrameImage,
                HeaderLabel,
            }
        };
    }

    //View component Initialization
    private void InitializeComponents()
    {
        HeaderLabel = new Label()
        {
            FontSize = 32,
            Text = "Sign up",
            FontAttributes = FontAttributes.Bold,
            HorizontalTextAlignment = TextAlignment.Center,
        };

        var headerImage = new Image
        {
            HeightRequest = 200,
            WidthRequest = 200,
            Source = "sign_up.png",
            Aspect = Aspect.AspectFit,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        HeaderFrameImage = new Frame
        {
            Padding = 0,
            CornerRadius = 120,
            WidthRequest = 250,
            HeightRequest = 250,
            Content = headerImage,
            IsClippedToBounds = true,
            BackgroundColor = Colors.GhostWhite,
            HorizontalOptions = LayoutOptions.Center,
        };
    }
}
