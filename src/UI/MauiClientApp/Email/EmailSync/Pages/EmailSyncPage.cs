using MauiClientApp.Email.EmailSync.ViewModels;
using MauiClientApp.Email.EmailSync.Views;

namespace MauiClientApp.Email.EmailSync.Pages;

public class EmailSyncPage: EmailPage<EmailSyncViewModel>
{
    //View components
    private Label PageTitleLabel = null!;
    
    //Construction
    public EmailSyncPage(EmailSyncViewModel viewModel) : base(viewModel, new VerticalStackLayout())
    {
        InitializePageLabel();
    }

    protected override ScrollView PageContent => new()
    {
        Content = new EmailSyncIndicatorView()
    };

    //View component Initialization
    private void InitializePageLabel()
    {
        PageTitleLabel = new Label
        {
            Text = "Umbrella",
            FontSize = 26,
            FontAttributes = FontAttributes.Bold,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        Shell.SetTitleView(this, PageTitleLabel);
    }
}
