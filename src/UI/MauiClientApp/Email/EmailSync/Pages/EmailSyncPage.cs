using MauiClientApp.Email.EmailSync.ViewModels;

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
                new ActivityIndicator
                {
                    IsRunning = true,
                    HeightRequest = 200,
                    WidthRequest = 200,
                },
                new Label()
                { 
                    Text = "Loading...",
                    MaxLines = 1,
                    FontSize = 28,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                }
            }
        }
    };
}
