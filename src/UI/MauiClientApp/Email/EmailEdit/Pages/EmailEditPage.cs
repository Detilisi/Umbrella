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
            Command = new Command(async () => await BindingContext.SendEmailCommand.ExecuteAsync(EmailEditPage.ViewInputValues))
        };

        ToolbarItems.Add(sendToolbarItem);
    }
    
    private static void InitializeViewComponents()
    {
        SenderEmailEntry = new Entry{ Placeholder = "From:" };
        SenderEmailEntry.DynamicResource(StyleProperty, "EmailEntry");

        RecipientsEmailsEntry = new Entry{ Placeholder = "To:" };
        RecipientsEmailsEntry.DynamicResource(StyleProperty, "EmailEntry");

        SubjectLineEntry = new Entry{ Placeholder = "Subject:" };
        SubjectLineEntry.DynamicResource(StyleProperty, "EmailEntry");

        BodyTextEditor = new Editor
        {
            Placeholder = "Body:",
            AutoSize = EditorAutoSizeOption.TextChanges
        };
        BodyTextEditor.DynamicResource(StyleProperty, "EmailEditor");
    }

    private static EmailModel ViewInputValues => new()
    {
        Sender = SenderEmailEntry.Text,
        SenderName = SenderEmailEntry.Text,
        Recipients = [RecipientsEmailsEntry.Text],
        Subject = SubjectLineEntry.Text,
        Body = BodyTextEditor.Text,
        EmailType = Domain.Email.Entities.Enums.EmailType.Draft
    };
    
}
