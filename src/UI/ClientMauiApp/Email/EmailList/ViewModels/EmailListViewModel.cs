namespace MauiClientApp.Email.EmailList.ViewModels;

public class EmailListViewModel(ViewModel chatViewModel) : EmailViewModel(chatViewModel)
{
    public ObservableCollection<EmailModel> EmailMessageList { get; set; } = [];

    public override void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        base.OnViewModelStarting(cancellationToken);
    }
}

