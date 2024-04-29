namespace MauiClientApp.Email.EmailList.ViewModels;

public class EmailListViewModel(IEmailFetcher emailFetcher) : ViewModel
{
    private readonly IEmailFetcher _emailFetcher = emailFetcher;

    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    public override async void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        base.OnViewModelStarting(cancellationToken);
        await LoadEmailsAsync(cancellationToken);
    }

    private async Task LoadEmailsAsync(CancellationToken cancellationToken = default)
    {
        // Establish connection with dummy credentials (replace with proper logic)
        var currentUser = new UserModel
        {
            EmailAddress = "test@domain.com",
            EmailPassword = "password"
        };

        await _emailFetcher.ConnectAsync(currentUser, cancellationToken);

        // Retrieve email messages
        var allEmails = await _emailFetcher.LoadEmailsAsync(cancellationToken);
        if (allEmails.IsFailure) return;

        foreach (var email in allEmails.Value) 
        {
            EmailMessageList.Add(email);
        }
    }
}

