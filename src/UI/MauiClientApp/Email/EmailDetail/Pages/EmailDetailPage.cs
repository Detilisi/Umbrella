using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

internal class EmailDetailPage(EmailDetailViewModel viewModel) : EmailPage<EmailDetailViewModel>(viewModel)
{
    //View components
    private Label SubjectLabel = null!;
    private Label BodyTextLabel = null!;
    private BoxView SeparatorBoxView = null!;
    private EmailSenderView EmailSenderView = null!;

    //Construction
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
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var currentEmail = BindingContext.CurrentEmail;
        EmailSenderView = new(currentEmail.SenderName??currentEmail.Sender, currentEmail.CreatedAt);

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
