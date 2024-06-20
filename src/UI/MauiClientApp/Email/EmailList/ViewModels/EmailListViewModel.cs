using Application.Email.Features.Queries.GetEmailList;
using MauiClientApp.Common.Enums;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Properties
    private bool ShouldKeepConversation { get; set; }
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);
        await LoadEmailsAsync(token);
        await InitializeConvernsation();
    }

    //Load methods
    private async Task LoadEmailsAsync(CancellationToken token)
    {
        var userId = 1;
        var loadEmailQuery = new GetEmailListQuery(userId);
        var emailList = await _mediator.Send(loadEmailQuery, token);
        if (emailList.IsFailure) return;

        EmailMessageList.Clear();
        foreach (var emailModel in emailList.Value)
        {
            EmailMessageList.Add(emailModel);
        }
    }

    //Commands
    [RelayCommand]
    public async Task OpenEmail(EmailModel selectedEmail)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            [nameof(EmailModel)] = selectedEmail
        };

        await NavigationService.NavigateToViewModelAsync<EmailDetailViewModel>(navigationParameter);
    }

    [RelayCommand]
    public async Task WriteEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailEditViewModel>();
    }

    //UI method
    private async Task InitializeConvernsation()
    {
        ShouldKeepConversation = true;

        //Intro
        await SpeechService.SpeakAsync("Hello,my name is Umbrella, your voice operated emailing system.");
        await SpeechService.SpeakAsync("Please let me know how I can help you?");
        await GetAndHandleUserInput();

        while (ShouldKeepConversation)
        {
            //Announce option
            await SpeechService.SpeakAsync($"You currently have {EmailMessageList.Count} new messages.");
            await SpeechService.SpeakAsync("Would you like me to read your messages or help you write an email?");
            await GetAndHandleUserInput();
        }
    }

    //Helpers
    private async Task GetAndHandleUserInput()
    {
        //Get user input
        var userInput = await SpeechService.ListenAsync();
        await SpeechService.StopListenAsync();

        //Process
        if (!string.IsNullOrEmpty(userInput))
        {
            //Get intent
            var userIntent = IntentRecognizer.GetIntent(userInput);

            //Perform intent
            if (userIntent == UserIntent.WriteEmail)
            {
                await WriteEmailCommand.ExecuteAsync(null);
                ShouldKeepConversation = false;
                return;
            }
            if (userIntent == UserIntent.ReadEmails)
            {
                ShouldKeepConversation = false;
                //Skim through all message
                for (var i = 0; i < EmailMessageList.Count; i++)
                {
                    var message = EmailMessageList[i];
                    await SpeechService.SpeakAsync($"Message {i + 1} is from {message.SenderName}, subject {message.Subject}");
                }
            }
        }
    }
}

