using MauiClientApp.Email.EmailDetail.ViewModels;

namespace MauiClientApp.Email.EmailDetail.Pages;

public class EmailDetailPage: EmailPage<EmailDetailViewModel>
{
    //View components
    private static Label SubjectLabel = null!;
    private static Label BodyTextLabel = null!;
    //private EmailControlView? EmailControls;
    private static BoxView SeparatorBoxView = null!;

    //Construction
    public EmailDetailPage(EmailDetailViewModel viewModel) : base(viewModel, new VerticalStackLayout())
    {
        InitializeViewComponents();
    }

    protected override ScrollView PageContent => throw new NotImplementedException();

    //View component Initialization
    private void InitializeViewComponents()
    {
        SubjectLabel = new()
        {
            Text = BindingContext.CurrentEmail.Subject
        };
        BodyTextLabel = new()
        {
            Text = BindingContext.CurrentEmail.Body
        };
        SeparatorBoxView = new()
        {
            Margin = new Thickness(10, 0, 10, 0)
        };

        SubjectLabel.DynamicResource(View.StyleProperty, "EmailDetailPageSubjectLabel");
        BodyTextLabel.DynamicResource(View.StyleProperty, "EmailDetailPageBodyTextLabel");
        SeparatorBoxView.DynamicResource(View.StyleProperty, "EmailDataTemplateSeparator");
    }
}
