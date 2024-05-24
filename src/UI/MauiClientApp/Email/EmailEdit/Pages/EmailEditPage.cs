namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage : EmailPage<EmailEditViewModell>
{
    //View components
    private static Entry SenderEmailEntry = null!;
    private static Entry RecipientsEmailsEntry = null!;

    private static Entry SubjectLineEntry = null!;
    private static Editor BodyTextEditor = null!;

    //Construction
    public EmailEditPage(EmailEditViewModell viewModel) : base(viewModel)
    {
        InitializeShell();
        EmailEditPage.InitializeViewComponents();
    }
    protected override ScrollView PageContent => new()
    {
        Padding = 10,
        Content = new VerticalStackLayout()
        {
            SenderEmailEntry,
            RecipientsEmailsEntry,
            SubjectLineEntry,
            BodyTextEditor
        }
    };

    //View component Initialization
    private void InitializeShell()
    {
        Title = "Draft";
        var sendToolbarItem = new ToolbarItem
        {
            IconImageSource = new FontImageSource
            {
                Size = 30,
                FontFamily = "FontAwesomeSolid",
                Glyph = FontAwesomeIcons.PaperPlane
            },
            Command = BindingContext.SendEmailCommand
        };

        ToolbarItems.Add(sendToolbarItem);
    }
    
    private static void InitializeViewComponents()
    {
        RecipientsEmailsEntry = new Entry { Placeholder = "To:" };
        RecipientsEmailsEntry.DynamicResource(StyleProperty, "EmailEntry");

        SenderEmailEntry = new Entry{ Placeholder = "From:" };
        SenderEmailEntry.DynamicResource(StyleProperty, "EmailEntry");
        SenderEmailEntry.Bind(Entry.TextProperty, static (EmailEditViewModell vm) => vm.EmailDraft.Sender, 
            static (EmailEditViewModell vm, string text) => vm.EmailDraft.Sender = text);

        SubjectLineEntry = new Entry{ Placeholder = "Subject:" };
        SubjectLineEntry.DynamicResource(StyleProperty, "EmailEntry");
        SubjectLineEntry.Bind(Entry.TextProperty, static (EmailEditViewModell vm) => vm.EmailDraft.Subject,
            static (EmailEditViewModell vm, string text) => vm.EmailDraft.Subject = text);

        BodyTextEditor = new Editor
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges
        };
        BodyTextEditor.DynamicResource(StyleProperty, "EmailEditor");
        BodyTextEditor.Bind(Editor.TextProperty, static (EmailEditViewModell vm) => vm.EmailDraft.Body,
            static (EmailEditViewModell vm, string text) => vm.EmailDraft.Body = text);
    }
}
