using MauiClientApp.Email.EmailDetail.ViewModels;
using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

public class EmailDetailPage: EmailPage<EmailDetailViewModel>
{
    //View components
    private static Label SubjectLabel = null!;
    private static Label BodyTextLabel = null!;
    private EmailSenderView EmailSenderView = null!;
    private static BoxView SeparatorBoxView = null!;

    //Construction
    public EmailDetailPage(EmailDetailViewModel viewModel) : base(viewModel, new VerticalStackLayout())
    {
        var currentEmail = BindingContext.CurrentEmail;
        EmailSenderView = new(currentEmail.SenderName, currentEmail.CreatedAt);

        InitializeViewComponents();
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
        
        SubjectLabel.DynamicResource(View.StyleProperty, "EmailDetailPageSubjectLabel");
        BodyTextLabel.DynamicResource(View.StyleProperty, "EmailDetailPageBodyTextLabel");
        SeparatorBoxView.DynamicResource(View.StyleProperty, "EmailDataTemplateSeparator");
    }
}
