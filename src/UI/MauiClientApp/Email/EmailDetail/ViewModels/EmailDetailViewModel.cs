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
        await SpeechService.SpeakAsync(UiStrings.ReadingInfo_Introduction);
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_EmailSummary, 
            CurrentEmail.SenderName, CurrentEmail.CreatedAt.ToString("F")));
        await SpeechService.SpeakAsync(string.Format(UiStrings.ReadingInfo_Subject, CurrentEmail.Subject));

        //Get user inpit 
        await SpeechService.SpeakAsync(UiStrings.ReadingQuery_ReadEmail);
        var userIntent = await ListenAndUserIntent();
        if (userIntent == UserIntent.Yes)
        {
            read:  await SpeechService.SpeakAsync(UiStrings.ReadingReponse_ReadEmail);
            var result = Task.Run(async () => await SpeechService.SpeakAsync(CurrentEmail.Body)).Result;
            await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatRead);
            
            var userIntent1 = await ListenAndUserIntent();
            if(userIntent1 == UserIntent.Yes) goto read;
        }
        else 
        {
            await SpeechService.SpeakAsync(UiStrings.ReadingQuery_RepeatDelete);
            var userIntent2 = await ListenAndUserIntent();
            if (userIntent2 == UserIntent.ReplyEmail) await ReplyEmailCommand.ExecuteAsync(null);
            else if (userIntent2 == UserIntent.ForwardEmail) await ForwardEmailCommand.ExecuteAsync(null);
        }
    }
}
