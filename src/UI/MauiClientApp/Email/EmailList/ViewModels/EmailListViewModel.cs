using Application.Email.Features.Queries.GetEmailList;

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
        await HandleConvernsation();
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

    //
    private async Task HandleConvernsation()
    {
        //Announce option
        await SpeechService.SpeakAsync($"You have {EmailMessageList.Count} new messages.");
        await SpeechService.SpeakAsync($"Would you like to read your emails or compose a new email message?");

        //Get user input
        var userInput = await SpeechService.ListenAsync();

        //Process
        if (!string.IsNullOrEmpty(userInput)) 
        {
            //Get intent
            var writeIntent = userInput.ToLower().Contains("write");
            var readIntent = userInput.ToLower().Contains("read");

            //Perform intent
            if (writeIntent)
            {
                await WriteEmailCommand.ExecuteAsync(writeIntent);
                return;
            }
        }

        await HandleConvernsation();
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
}

