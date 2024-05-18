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
    private void InitializeViewComponents()
    {   
        SenderEmailEntry = new()
        {
            Placeholder = "From:",
            Text = BindingContext.CurrentEditEmail.Sender,
        };

        RecipientsEmailsEntry = new()
        {
            Placeholder = "To:",
            Text = BindingContext.CurrentEditEmail.Sender,
        };

        SubjectLineEntry = new()
        {
            Placeholder = "Subject:",
            Text = BindingContext.CurrentEditEmail.Subject,
        };

        BodyTextEditor = new()
        {
            HeightRequest = 400,
            Placeholder = "Body:",
            Text = BindingContext.CurrentEditEmail.Body
        };
    }
}
