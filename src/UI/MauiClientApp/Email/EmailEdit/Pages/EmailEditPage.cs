using MauiClientApp.Email.EmailEdit.ViewModels;

namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage(EmailEditViewModell viewModel) : EmailPage<EmailEditViewModell>(viewModel)
{
    //View components
    private static Grid ContentGrid = null!;
    private static Label SenderEmailLabel = null!;
    private static Label RecipientsEmailsLabel = null!;

    private static Label SubjectLineLabel = null!;
    private static Editor BodyTextEditor = null!;

    protected override ScrollView PageContent => throw new NotImplementedException();
}
