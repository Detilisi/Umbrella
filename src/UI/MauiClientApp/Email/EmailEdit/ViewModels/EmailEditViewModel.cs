namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    public EmailModel CurrentEditEmail { get; set; } = null!;
}
