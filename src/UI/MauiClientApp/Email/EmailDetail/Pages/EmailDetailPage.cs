using MauiClientApp.Email.EmailDetail.ViewModels;
using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

public class EmailDetailPage: EmailPage<EmailDetailViewModel>
{
    //View components
    private static Label SubjectLabel = null!;
    private static Label BodyTextLabel = null!;
    private static BoxView SeparatorBoxView = null!;
    private EmailSenderView EmailSenderView = null!;

    //Construction
    public EmailDetailPage(EmailDetailViewModel viewModel) : base(viewModel)
    {
    }

    protected override ScrollView PageContent => new()
    {
        Padding = 10,
        Content = new VerticalStackLayout()
        {
            SubjectLabel,
            EmailSenderView,
            SeparatorBoxView,
            BodyTextLabel
        }
    };

    //Initialization
    protected override void OnAppearing()
    {
        return;
        InitializeEmailPage();

        base.OnAppearing();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var currentEmail = BindingContext.CurrentEmail;
        EmailSenderView = new(currentEmail.SenderName, currentEmail.CreatedAt);

        InitializeViewComponents();
        InitializeEmailPage();
    }

    //View component Initialization
    private void InitializeViewComponents()
    {
        SeparatorBoxView = new();
        SubjectLabel = new()
        {
            Text = BindingContext.CurrentEmail.Subject
        };
        BodyTextLabel = new()
        {
            Text = BindingContext.CurrentEmail.Body
        };
        
        SubjectLabel.DynamicResource(View.StyleProperty, "EmailSubjectLabel");
        BodyTextLabel.DynamicResource(View.StyleProperty, "EmailBodyTextLabel");
        SeparatorBoxView.DynamicResource(View.StyleProperty, "SeparatorLine");
    }
}
