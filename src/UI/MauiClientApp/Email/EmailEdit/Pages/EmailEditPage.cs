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
        InitializeViewComponents();
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
    
    private void InitializeViewComponents()
    {
        SenderEmailEntry = new Entry{ Placeholder = "From:" };
        SenderEmailEntry.DynamicResource(StyleProperty, "EmailEntry");
        SenderEmailEntry.SetBinding(Entry.TextProperty, nameof(BindingContext.CurrentEditEmail.Sender), mode: BindingMode.TwoWay);

        RecipientsEmailsEntry = new Entry{ Placeholder = "To:" };
        RecipientsEmailsEntry.DynamicResource(StyleProperty, "EmailEntry");
        RecipientsEmailsEntry.SetBinding(Entry.TextProperty, nameof(BindingContext.CurrentEditEmail.Recipients), mode: BindingMode.TwoWay);

        SubjectLineEntry = new Entry{ Placeholder = "Subject:" };
        SubjectLineEntry.DynamicResource(StyleProperty, "EmailEntry");
        SubjectLineEntry.SetBinding(Entry.TextProperty, nameof(BindingContext.CurrentEditEmail.Subject), mode: BindingMode.TwoWay);

        BodyTextEditor = new Editor
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges
        };
        BodyTextEditor.DynamicResource(StyleProperty, "EmailEditor");
        BodyTextEditor.SetBinding(Editor.TextProperty, nameof(BindingContext.CurrentEditEmail.Body), mode: BindingMode.TwoWay);     
    }
}
