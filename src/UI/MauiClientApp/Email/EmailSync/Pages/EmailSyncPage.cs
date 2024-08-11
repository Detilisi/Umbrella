using MauiClientApp.Email.EmailSync.Views;

namespace MauiClientApp.Email.EmailSync.Pages;

internal class EmailSyncPage: EmailPage<EmailSyncViewModel>
{
    //Construction
    public EmailSyncPage(EmailSyncViewModel viewModel) : base(viewModel)
    {
        Shell.SetTitleView(this, new Label
        {
            FontSize = 26,
            Text = "Umbrella",
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        });
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior() { IsVisible = false, IsEnabled = false });
    }

    protected override EmailSyncIndicatorView PageContent => new();
}
