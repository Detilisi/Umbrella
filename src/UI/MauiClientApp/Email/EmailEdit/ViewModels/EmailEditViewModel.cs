namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
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
            Sender = "sender@gmail.com",
            SenderName = string.Empty,
            Subject = string.Empty,
            Body = string.Empty,
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
