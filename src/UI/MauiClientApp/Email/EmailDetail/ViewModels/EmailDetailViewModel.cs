namespace MauiClientApp.Email.EmailDetail.ViewModels;

internal partial class EmailDetailViewModel(IMediator mediator) : EmailViewModel(mediator), IQueryAttributable
{
    //Properties
    public EmailModel CurrentEmail { get; set; } = null!;

    //Navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var selectedEmail = (EmailModel)query[nameof(EmailModel)];
        CurrentEmail = selectedEmail;
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
        if (userIntent == UserIntent.Affirm)
        {
            await SpeechService.SpeakAsync($"Sure thing, the email body reads: {CurrentEmail.Body}");
            await SpeechService.SpeakAsync("Do you want me to read it again?");

            var userIntent1 = await ListenAndUserIntent();
        }
    }
}
