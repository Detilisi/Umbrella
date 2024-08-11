namespace MauiClientApp.User.SignUp.Views;

internal class SignUpHeaderView : ContentView
{
    //Construction
    public SignUpHeaderView()
    {
        var headerImage = new Image
        {
            HeightRequest = 200,
            WidthRequest = 200,
            Source = "sign_up.png",
            Aspect = Aspect.AspectFit,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = new VerticalStackLayout()
        {
            Spacing = 25,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                new Frame
                {
                    Padding = 0,
                    CornerRadius = 120,
                    WidthRequest = 250,
                    HeightRequest = 250,
                    Content = headerImage,
                    IsClippedToBounds = true,
                    BackgroundColor = Colors.White,
                    HorizontalOptions = LayoutOptions.Center,
                },
                new Label(){ Text = "Sign up" }.DynamicResource(StyleProperty, "SignUpHeaderLabel"),
            }
        };
    }
}
