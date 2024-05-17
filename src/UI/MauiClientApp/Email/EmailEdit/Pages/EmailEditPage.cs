using MauiClientApp.Email.EmailEdit.ViewModels;

namespace MauiClientApp.Email.EmailEdit.Pages;

internal class EmailEditPage : EmailPage<EmailEditViewModell>
{
    public EmailEditPage(EmailEditViewModell viewModel) : base(viewModel)
    {
    }

    protected override ScrollView PageContent => throw new NotImplementedException();
}
