using Application.Email.Features.Commands.SyncInbox;

namespace MauiClientApp.Email.EmailSync.ViewModels;

internal class EmailSyncViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //Synx emails
        await SyncEmailInboxAsync(token);
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }

    //Sync methods
    private async Task SyncEmailInboxAsync(CancellationToken token)
    {
        var syncCommand = new SyncInboxCommand();

        var syncResult = await _mediator.Send(syncCommand, token);
        if (syncResult.IsFailure) return; // handle error
    }
}
