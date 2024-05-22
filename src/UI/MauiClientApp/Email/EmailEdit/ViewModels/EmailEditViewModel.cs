namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    [ObservableProperty]
    public EmailModel currentEditEmail = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //Initialize edit email
        var currentUser = new UserModel() //Must get from UserSessionService
        { 
            EmailAddress = string.Empty, 
            EmailPassword = string.Empty
        }; 

        CurrentEditEmail = new() 
        {
            Sender = currentUser.EmailAddress,
            SenderName = currentUser.EmailAddress,
            Recipients = [],
            Subject = string.Empty,
            Body = string.Empty,
            EmailStatus = Domain.Email.Entities.Enums.EmailStatus.Draft
        };
    }

    //Commands
    [RelayCommand]
    public async Task SendEmail()
    {
        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }
}
