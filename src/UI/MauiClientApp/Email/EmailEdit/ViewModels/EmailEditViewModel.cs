namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);
    }

    //Commands
    [RelayCommand]
    public static async Task SendEmail(EmailModel emailModel)
    {
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }
}
