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
    protected override VerticalStackLayout PageContent => new()
    {
        Padding = 10,
        Children =
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

        var currentEmail = ViewModel.CurrentEmail;
        EmailSenderView = new(currentEmail.SenderName??currentEmail.Sender, currentEmail.CreatedAt);

        InitializeToolBar();
        InitializeViewComponents();
        InitializeEmailPage();
    }

    //View component Initialization
    private void InitializeToolBar()
    {
        Title = "Email";
        var replyToolbarItem = new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.Reply
            },
            Command = new Command(async () => await ViewModel.ReplyEmailCommand.ExecuteAsync(ViewModel.CurrentEmail))
        };
        var deleteToolbarItem = new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.TrashCan
            },
            Command = new Command(async () => await ViewModel.DeleteEmailCommand.ExecuteAsync(ViewModel.CurrentEmail))
        };

        ToolbarItems.Add(deleteToolbarItem);
        ToolbarItems.Add(replyToolbarItem);
    }
    private void InitializeViewComponents()
    {
        SeparatorBoxView = new();
        SubjectLabel = new()
        {
            Text = ViewModel.CurrentEmail.Subject
        };
        BodyTextLabel = new()
        {
            Text = ViewModel.CurrentEmail.Body
        };
        
        SubjectLabel.DynamicResource(View.StyleProperty, "EmailSubjectLabel");
        BodyTextLabel.DynamicResource(View.StyleProperty, "EmailBodyTextLabel");
        SeparatorBoxView.DynamicResource(View.StyleProperty, "SeparatorLine");
    }
}
