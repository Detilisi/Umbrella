namespace MauiClientApp.Email.EmailDetail.ViewModels;

internal partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Navigation
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        CurrentEmail = selectedEmail;
        await StartVMConversationm();
    }

    //Commands
    [RelayCommand]
    public async Task ReplyEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    [RelayCommand]
    public async Task ForwardEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }
    //VM conversationm
    private async Task StartVMConversationm()
    {
        //Introduction
        await SpeechService.SpeakAsync("Welcome to the email reading page");
        await SpeechService.SpeakAsync($"This message was sent by {CurrentEmail.SenderName} at {CurrentEmail.CreatedAt}");
        await SpeechService.SpeakAsync($"Email subject line: {CurrentEmail.Subject}");

        //Get user inpit 
        await SpeechService.SpeakAsync("Should I proceed reading this message?");
        var userIntent = await ListenAndUserIntent();
        if (userIntent == UserIntent.Yes)
        {
            

            read: var result = Task.Run(async () => await SpeechService.SpeakAsync($"Sure thing, the email body reads: {CurrentEmail.Body}")).Result;
            await SpeechService.SpeakAsync("Do you want me to read it again?");
            
            var userIntent1 = await ListenAndUserIntent();
            if(userIntent1 == UserIntent.Yes) goto read;
        }
        else 
        {
            await SpeechService.SpeakAsync("Do you want to reply, forward, or delete this message?");
            var userIntent2 = await ListenAndUserIntent();
            if (userIntent2 == UserIntent.ReplyEmail) await ReplyEmailCommand.ExecuteAsync(null);
            else if (userIntent2 == UserIntent.ForwardEmail) await ForwardEmailCommand.ExecuteAsync(null);
        }
    }
}
