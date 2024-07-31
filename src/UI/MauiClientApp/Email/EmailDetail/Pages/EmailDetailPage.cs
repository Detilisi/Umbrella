using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

internal class EmailDetailPage(EmailDetailViewModel viewModel) : EmailPage<EmailDetailViewModel>(viewModel)
{
    //Construction
    protected override VerticalStackLayout PageContent
    {
        get
        {
            var test = ViewModel.CurrentEmail;
            return new VerticalStackLayout()
            {
                Padding = 10,
                Children =
                {
                    new Label(){ Text = ViewModel.CurrentEmail.Subject}.DynamicResource(View.StyleProperty, "EmailSubjectLabel"),
                    new EmailSenderView(ViewModel.CurrentEmail.SenderName ?? ViewModel.CurrentEmail.Sender, ViewModel.CurrentEmail.CreatedAt),
                    new SeparatorLine(),
                    new Label(){ Text = ViewModel.CurrentEmail.Body }.DynamicResource(View.StyleProperty, "EmailBodyTextLabel")
                }
            };
        }
    }

    //Initialization
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        var currentEmail = ViewModel.CurrentEmail;
        //EmailSenderView = new(currentEmail.SenderName??currentEmail.Sender, currentEmail.CreatedAt);

        InitializeToolBar();
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
}
