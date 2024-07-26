using Application.Email.Features.Commands.SyncInbox;
using EmailViewModel = MauiClientApp.Email.Base.ViewModels.EmailViewModel;

namespace MauiClientApp.Email.EmailSync.ViewModels;

internal class EmailSyncViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Life cycle 
    public override async void OnViewModelStarting()
    {
        base.OnViewModelStarting();

        //Synx emails
        await SyncEmailInboxAsync();
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }

    //Sync methods
    private async Task SyncEmailInboxAsync()
    {
        var syncCommand = new SyncInboxCommand();

        var syncResult = await Mediator.Send(syncCommand);
        if (syncResult.IsFailure) return; // handle error
    }
}
