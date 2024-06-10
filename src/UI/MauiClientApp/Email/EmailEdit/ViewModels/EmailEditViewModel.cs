using Application.Email.Features.Commands.SendEmail;
using Application.User.Abstractions.Services;

namespace MauiClientApp.Email.EmailEdit.ViewModels;

internal partial class EmailEditViewModel(IMediator mediator, IUserSessionService userSessionService) : EmailViewModel(mediator)
{
    //Fields
    private readonly IUserSessionService _userSessionService = userSessionService;

    //Properties
    [ObservableProperty]
    public EmailModel emailDraft = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        var currentUserResult = _userSessionService.GetCurrentSession();
        if (currentUserResult.IsFailure) return; //Handle error

        EmailDraft = new() 
        {
            Recipients = [],
            Body = string.Empty,
            Subject = string.Empty,
            SenderName = string.Empty,
            Sender = currentUserResult.Value.EmailAddress,
        };
    }

    //Commands
    [RelayCommand]
    public async Task SendEmail()
    {
        var sendCommand = new SendEmailCommand(EmailDraft);
        var sendEmailResult = await _mediator.Send(sendCommand);
        if (sendEmailResult.IsFailure) return; // handle error

        await NavigationService.NavigateToViewModelAsync<EmailListViewModel>();
    }
}
