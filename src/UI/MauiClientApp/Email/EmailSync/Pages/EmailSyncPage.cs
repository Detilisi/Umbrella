using MauiClientApp.Email.EmailSync.Views;

namespace MauiClientApp.Email.EmailSync.Pages;

internal class EmailSyncPage: EmailPage<EmailSyncViewModel>
{
    //View components
    private Label PageTitleLabel = null!;
    
    //Construction
    public EmailSyncPage(EmailSyncViewModel viewModel) : base(viewModel)
    {
        InitializeViewComponents();
    }

    protected override ScrollView PageContent => new()
    {
        Content = new EmailSyncIndicatorView()
    };

    //View life-cycle
    protected override void OnAppearing()
    {
        base.OnAppearing();

        ChatHistory.IsVisible = false;
    }

    //View component Initialization
    private void InitializeViewComponents()
    {
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            IsVisible = false,
            IsEnabled = false
        });

        PageTitleLabel = new Label
        {
            Text = "Umbrella",
            FontSize = 26,
            TextColor = Colors.White,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        Shell.SetTitleView(this, PageTitleLabel);
    }
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
