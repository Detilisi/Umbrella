namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModel(IMediator mediator, IEmailSender emailSender) : EmailViewModel(mediator, default)
{
    //Fields
    private readonly IEmailSender _emailSender = emailSender;

    //Properties
    [ObservableProperty]
    public EmailModel emailDraft = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        EmailDraft = new() 
        {
            Recipients = [],
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
        var connectResult = await _emailSender.ConnectAsync(UserSessionService.CurrentUser);
        if (connectResult.IsFailure) return;//Handle error
        
        var sendResult = await _emailSender.SendEmailAsync(EmailDraft);
        if (sendResult.IsFailure) return;//Handle error

        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }
}
