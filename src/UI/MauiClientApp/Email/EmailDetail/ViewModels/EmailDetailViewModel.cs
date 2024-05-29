namespace MauiClientApp.Email.EmailDetail.ViewModels;

public partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator, default), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        CurrentEmail = selectedEmail;
    }
}
