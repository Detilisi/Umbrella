namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    public EmailModel CurrentEditEmail { get; set; } = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //load emails
        CurrentEditEmail = new() 
        {
            Sender = "",
            SenderName = "",
            Recipients = [],
            Subject = "",
            Body = "",
            EmailStatus = Domain.Email.Entities.Enums.EmailStatus.Draft
        };
    }
}
