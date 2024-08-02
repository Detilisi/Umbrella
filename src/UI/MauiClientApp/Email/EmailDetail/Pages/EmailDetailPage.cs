using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

internal class EmailDetailPage : EmailPage<EmailDetailViewModel>
{
    private EmailSenderView SenderView = null!;

    //Construction
    public EmailDetailPage(EmailDetailViewModel viewModel) : base(viewModel)
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
        ToolbarItems.Add(replyToolbarItem);
    }

    protected override VerticalStackLayout PageContent => new()
    {
        Padding = 10,
        Children =
        {
            new Label()
                .DynamicResource(View.StyleProperty, "EmailSubjectLabel")
                .Bind(Label.TextProperty, static (EmailDetailViewModel vm) => vm.Subject, mode: BindingMode.OneWay),

            new EmailSenderView(ViewModel.Sender, ViewModel.SentAtDate),
            new SeparatorLine(),

            new Editor(){ IsReadOnly = true }
                .DynamicResource(StyleProperty, "EmailEditor")
                .Bind(Editor.TextProperty, static (EmailDetailViewModel vm) => vm.Body, mode: BindingMode.OneWay)
        }
    };
    //Initialization
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        InitializeEmailPage();
    }
}
