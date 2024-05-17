namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModell(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    public EmailModel CurrentEditEmail { get; set; } = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        //Initialize edit email
        var currentUser = new UserModel() //Must get from UserSessionService
        { 
            EmailAddress = "test@test.com", 
            EmailPassword = ""
        }; 

        CurrentEditEmail = new() 
        {
            Sender = currentUser.EmailAddress,
            SenderName = currentUser.EmailAddress,
            Recipients = [],
            Subject = "",
            Body = "",
            EmailStatus = Domain.Email.Entities.Enums.EmailStatus.Draft
        };
    }

    //Commands
    [RelayCommand]
    public Task SendEmail()
    {
        return Task.CompletedTask;
    }
}
