using Application.Email.Features.Queries.GetEmailById;

namespace MauiClientApp.Email.EmailDetail.ViewModels;

public partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator, default), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Load methods
    private async Task LoadCurrentEmailAsync(CancellationToken token)
    {
        var userId = 1;
        var loadEmailQuery = new GetEmailByIdQuery(userId, 1);
        var emailMessage = await _mediator.Send(loadEmailQuery, token);
        if (emailMessage.IsFailure) return;

        CurrentEmail = emailMessage.Value;
    }

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        CurrentEmail = selectedEmail;
    }
}
