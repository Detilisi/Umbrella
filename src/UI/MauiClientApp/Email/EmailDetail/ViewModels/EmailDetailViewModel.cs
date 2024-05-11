using Application.Email.Features.Queries.GetEmailById;
using Application.Email.Features.Queries.GetEmailList;

namespace MauiClientApp.Email.EmailDetail.ViewModels;

public class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        await LoadCurrentEmailAsync(token);
    }

    //Load methods
    private async Task LoadCurrentEmailAsync(CancellationToken token)
    {
        var userId = 1;
        var loadEmailQuery = new GetEmailByIdQuery(userId, 1);
        var emailMessage = await _mediator.Send(loadEmailQuery, token);
        if (emailMessage.IsFailure) return;

        CurrentEmail = emailMessage.Value;
    }
}
