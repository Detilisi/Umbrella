using MauiClientApp.Email.EmailDetail.Views;

namespace MauiClientApp.Email.EmailDetail.Pages;

internal class EmailDetailPage : EmailPage<EmailDetailViewModel>
{
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

    protected override ScrollView PageContent => new()
    {
        Padding = 10,
        Content = new VerticalStackLayout()
        {
            //Padding = 10,
            Children =
            {
                new Label()
                    .DynamicResource(View.StyleProperty, "EmailSubjectLabel")
                    .Bind(Label.TextProperty, static (EmailDetailViewModel vm) => vm.Subject, mode: BindingMode.OneWay),

                new EmailSenderView()
                    .Bind(EmailSenderView.EmailSenderProperty, static (EmailDetailViewModel vm) => vm.Sender, mode: BindingMode.OneWay)
                    .Bind(EmailSenderView.EmailSentDateProperty, static (EmailDetailViewModel vm) => vm.SentAtDate, mode: BindingMode.OneWay),

                new SeparatorLine(),
                new ScrollView()
                {
                    Content = new Label()
                        .DynamicResource(View.StyleProperty, "EmailBodyLabel")
                        .Bind(Label.TextProperty, static (EmailDetailViewModel vm) => vm.Body, mode: BindingMode.OneWay)
                }
            }
        }
    };
}
