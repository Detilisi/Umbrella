namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage(EmailEditViewModel viewModel) : EmailPage<EmailEditViewModel>(viewModel)
{
    //Construction
    protected override void InitializeEmailPage()
    {
        Title = "Draft";
        ToolbarItems.Add(new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.PaperPlane
            },
            Command = ViewModel.SendEmailCommand
        });

        base.InitializeEmailPage();
    }

    protected override VerticalStackLayout PageContent
    {
        get
        {
            var senderEmailEntry = new Entry { Placeholder = "From:" }
                .DynamicResource(StyleProperty, "EmailEntry")
                .Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Sender, static (EmailEditViewModel vm, string text) => vm.Sender = text);

            var subjectLineEntry = new Entry { Placeholder = "Subject:" }
                .DynamicResource(StyleProperty, "EmailEntry")
                .Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Subject, static (EmailEditViewModel vm, string text) => vm.Subject = text);

            var recipientEmailsEntry = new Entry { Placeholder = "To:" }
                .DynamicResource(StyleProperty, "EmailEntry")
                .Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Recipient,static (EmailEditViewModel vm, string text) => vm.Recipient = text);

            var bodyTextEditor = new Editor{ Placeholder = "Body:", AutoSize = EditorAutoSizeOption.TextChanges}
                .DynamicResource(StyleProperty, "EmailEditor")
                .Bind(Editor.TextProperty, static (EmailEditViewModel vm) => vm.Body, static (EmailEditViewModel vm, string text) => vm.Body = text);

            return new VerticalStackLayout()
            {
                Padding = 10,
                Children =
                {
                    senderEmailEntry,
                    recipientEmailsEntry,
                    subjectLineEntry,
                    bodyTextEditor
                }
            };
        }
    }
}
