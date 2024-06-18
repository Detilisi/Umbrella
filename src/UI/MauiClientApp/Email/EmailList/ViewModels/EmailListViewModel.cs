using Application.Email.Features.Queries.GetEmailList;
using MauiClientApp.Common.Enums;

namespace MauiClientApp.Email.EmailList.ViewModels;

internal partial class EmailListViewModel(IMediator mediator) : EmailViewModel(mediator)
{
    //Properties
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //load emails
        await LoadEmailsAsync(token);

        //Start convo
        await InitializeConvernsation();
    }

    //Load methods
    private async Task LoadEmailsAsync(CancellationToken token)
    {
        var userId = 1;
        var loadEmailQuery = new GetEmailListQuery(userId);
        var emailList = await _mediator.Send(loadEmailQuery, token);
        if (emailList.IsFailure) return;

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
        //Intro
        await SpeechService.SpeakAsync("Hello,my name is Umbrella, your voice operated emailing system.");
        await SpeechService.SpeakAsync("Please let me know how I can help you?");

        //Get user input
        await GetAndHandleUserInput();

        //Announce option
        await SpeechService.SpeakAsync($"You currently have {EmailMessageList.Count} new messages.");
        await SpeechService.SpeakAsync("Please let me know if you want me to read your messages, " +
            "or if you want me to help you write an email to a friend or boss?");

        //Get user input
        await GetAndHandleUserInput();

        //Repeat
        await InitializeConvernsation();
    }

    //Helpers
    private async Task GetAndHandleUserInput()
    {
        //Get user input
        var userInput = await SpeechService.ListenAsync();

        //Process
        if (!string.IsNullOrEmpty(userInput))
        {
            //Get intent
            var userIntent = IntentRecognizer.GetIntent(userInput);

            //Perform intent
            if (userIntent == UserIntent.WriteEmail)
            {
                await WriteEmailCommand.ExecuteAsync(null);
                return;
            }
            if (userIntent == UserIntent.ReadEmails)
            {
                //Skim through all message
                for (var i = 0; i < EmailMessageList.Count; i++)
                {
                    var message = EmailMessageList[i];
                    await SpeechService.SpeakAsync($@"Message {i} is from {message.SenderName}, subject {message.Subject}");
                }
            }
        }
    }
}

