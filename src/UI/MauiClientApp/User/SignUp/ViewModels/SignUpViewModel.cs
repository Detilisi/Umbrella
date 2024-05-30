using Application.User.Features.Commands.RegisterUser;

namespace MauiClientApp.User.SignUp.ViewModels;

internal partial class SignUpViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;

    //Properties
    public string UserEmail { set; get; } = null!;
    public string UserPassword { set; get; } = null!;

    //Commands
    [RelayCommand]
    public async Task RegisterUser()
    {
        if(string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(UserPassword)) return;

        var registerUsercommand = new RegisterUserCommand()
        {
            EmailAddress = UserEmail,
            EmailPassword = UserPassword,
            UserName = UserEmail
        };
        var registerUserResult = await _mediator.Send(registerUsercommand);
        if (registerUserResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailSyncViewModel>();
    }
}