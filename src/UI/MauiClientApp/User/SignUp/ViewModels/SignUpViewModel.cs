using Application.Common.Abstractions.Services;
using Application.User.Features.Commands.RegisterUser;
using Application.User.Features.Queries.AutoLoginUser;

namespace MauiClientApp.User.SignUp.ViewModels;

internal partial class SignUpViewModel(IMediator mediator, IEncryptionService encryptionService) : ViewModel
{
    //Fields
    protected readonly IMediator _mediator = mediator;
    private readonly IEncryptionService _encryptionService = encryptionService;

    //Properties
    public string UserEmail { set; get; } = null!;
    public string UserPassword { set; get; } = null!;

    //Life cycle 
    public override async void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        SpeechService.RequestPermissions();
        var autoLoginUserQuery = new AutoLoginUserQuery();
        var autoLoginUserResult = await _mediator.Send(autoLoginUserQuery);
        if (autoLoginUserResult.IsSuccess)
        {
            await NavigationService.NavigateToViewModelAsync<EmailSyncViewModel>();
        }
    }

    //Commands
    [RelayCommand]
    public async Task RegisterUser()
    {
        if(string.IsNullOrEmpty(UserEmail) || string.IsNullOrEmpty(UserPassword)) return;
        var encryptedPasssword = _encryptionService.Encrypt(UserPassword);
        var registerUsercommand = new RegisterUserCommand()
        {
            EmailAddress = UserEmail,
            EncrytedPassword = encryptedPasssword,
            UserName = UserEmail
        };
        var registerUserResult = await _mediator.Send(registerUsercommand);
        if (registerUserResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailSyncViewModel>();
    }
}