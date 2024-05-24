namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModel(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    [ObservableProperty]
    public EmailModel emailDraft = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        EmailDraft = new() 
        {
            Body = string.Empty,
            Subject = string.Empty,
            Sender = UserSessionService.CurrentUser.EmailAddress,
            SenderName = UserSessionService.CurrentUser.UserName,
        };
    }

    //Commands
    [RelayCommand]
    public async Task SendEmail()
    {
        var test = EmailDraft;
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }
}
