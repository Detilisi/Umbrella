using Application.Common.Abstractions.Services;
using Application.User.Features.Commands.RegisterUser;
using Application.User.Features.Queries.AutoLoginUser;

namespace MauiClientApp.User.SignUp.ViewModels;

internal partial class SignUpViewModel(IMediator mediator, IEncryptionService encryptionService) : ViewModel(mediator, isRootViewModel: true)
{
    //Fields
    private readonly IEncryptionService _encryptionService = encryptionService;

    //Properties
    public string UserEmail { set; get; } = null!;
    public string UserPassword { set; get; } = null!;

    //Life cycle 
    protected override async void ViewAppearing()
    {
        base.ViewAppearing();

        var autoLoginUserQuery = new AutoLoginUserQuery();
        var autoLoginUserResult = await Mediator.Send(autoLoginUserQuery);
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
        FireViewModelBusy();
        var encryptedPasssword = _encryptionService.Encrypt(UserPassword);
        var registerUsercommand = new RegisterUserCommand()
        {
            EmailAddress = UserEmail,
            EncrytedPassword = encryptedPasssword,
            UserName = UserEmail
        };
        var registerUserResult = await Mediator.Send(registerUsercommand);
        FireViewModelNotBusy();
        if (registerUserResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailSyncViewModel>();
    }
}