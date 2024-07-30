namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage(EmailEditViewModel viewModel) : EmailPage<EmailEditViewModel>(viewModel)
{
    //View components
    private Editor BodyTextEditor = null!;
    private Entry SubjectLineEntry = null!;
    private Entry SenderEmailEntry = null!;
    private Entry RecipientEmailsEntry = null!;

    //Construction
    protected override void InitializeEmailPage()
    {
        InitializeShell();
        InitializeViewComponents();

        base.InitializeEmailPage();
    }
    protected override VerticalStackLayout PageContent => new()
    {
        Padding= 10,
        Children = 
        {
            SenderEmailEntry,
            RecipientEmailsEntry,
            SubjectLineEntry,
            BodyTextEditor 
        }
    };

    //View component Initialization
    private void InitializeShell()
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
    }

    private void InitializeViewComponents()
    {
        SenderEmailEntry = new Entry{ Placeholder = "From:" };
        SenderEmailEntry.DynamicResource(StyleProperty, "EmailEntry");
        SenderEmailEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Sender, 
            static (EmailEditViewModel vm, string text) => vm.Sender = text);

        SubjectLineEntry = new Entry{ Placeholder = "Subject:" };
        SubjectLineEntry.DynamicResource(StyleProperty, "EmailEntry");
        SubjectLineEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Subject,
            static (EmailEditViewModel vm, string text) => vm.Subject = text);

        RecipientEmailsEntry = new Entry { Placeholder = "To:" };
        RecipientEmailsEntry.DynamicResource(StyleProperty, "EmailEntry");
        RecipientEmailsEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.Recipient,
            static (EmailEditViewModel vm, string text) => vm.Recipient = text);

        BodyTextEditor = new Editor
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges
        };
        BodyTextEditor.DynamicResource(StyleProperty, "EmailEditor");
        BodyTextEditor.Bind(Editor.TextProperty, static (EmailEditViewModel vm) => vm.Body,
            static (EmailEditViewModel vm, string text) => vm.Body = text);
    }
}
