using Application.Email.Features.Commands.SyncInbox;

namespace MauiClientApp.Email.EmailSync.ViewModels;

internal class EmailSyncViewModel(IMediator mediator) : EmailViewModel(mediator, isRootViewModel: true)
{
    //Life cycle 
    protected override async void ViewAppearing()
    {
        base.ViewAppearing();

        //Synx emails
        await SyncEmailInboxAsync();
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }

    //Sync methods
    private async Task SyncEmailInboxAsync()
    {
        await SpeechService.SpeakAsync("Sync in progress, please wait.");

        var syncResult = await Mediator.Send(new SyncInboxCommand());
        if (syncResult.IsFailure) return; // handle error
    }
}
