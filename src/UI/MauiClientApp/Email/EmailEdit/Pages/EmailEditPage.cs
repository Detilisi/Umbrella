namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage : EmailPage<EmailEditViewModel>
{
    //View components
    private Editor BodyTextEditor = null!;
    private Entry SubjectLineEntry = null!;
    private Entry SenderEmailEntry = null!;
    private Entry RecipientEmailsEntry = null!;

    //Construction
    public EmailEditPage(EmailEditViewModel viewModel) : base(viewModel)
    {
        InitializeShell();
        InitializeViewComponents();
    }
    protected override ScrollView PageContent => new()
    {
        Padding = 10,
        Content = new VerticalStackLayout()
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
        SenderEmailEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.EmailDraft.Sender, 
            static (EmailEditViewModel vm, string text) => vm.EmailDraft.Sender = text);

        SubjectLineEntry = new Entry{ Placeholder = "Subject:" };
        SubjectLineEntry.DynamicResource(StyleProperty, "EmailEntry");
        SubjectLineEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.EmailDraft.Subject,
            static (EmailEditViewModel vm, string text) => vm.EmailDraft.Subject = text);

        RecipientEmailsEntry = new Entry { Placeholder = "To:" };
        RecipientEmailsEntry.DynamicResource(StyleProperty, "EmailEntry");
        RecipientEmailsEntry.Bind(Entry.TextProperty, static (EmailEditViewModel vm) => vm.EmailDraft.Recipient,
            static (EmailEditViewModel vm, string text) => vm.EmailDraft.Recipient = text);

        BodyTextEditor = new Editor
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges
        };
        BodyTextEditor.DynamicResource(StyleProperty, "EmailEditor");
        BodyTextEditor.Bind(Editor.TextProperty, static (EmailEditViewModel vm) => vm.EmailDraft.Body,
            static (EmailEditViewModel vm, string text) => vm.EmailDraft.Body = text);
    }
}
