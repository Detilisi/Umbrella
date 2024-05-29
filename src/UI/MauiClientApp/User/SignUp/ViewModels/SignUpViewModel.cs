using Application.User.Features.Commands.RegisterUser;

namespace MauiClientApp.User.SignUp.ViewModels;

internal partial class SignUpViewModel(IMediator mediator) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;

    //Properties
    public UserModel NewUser { get; set; } = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        NewUser = new() { EmailAddress = ""};
    }

    //Commands
    [RelayCommand]
    public async Task RegisterUser()
    {
        var registerUsercommand = new RegisterUserCommand(NewUser);
        var registerUserResult = await _mediator.Send(registerUsercommand);
        if (registerUserResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailSyncViewModel>();
    }
}