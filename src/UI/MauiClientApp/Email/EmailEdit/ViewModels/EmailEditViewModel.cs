using Application.Email.Features.Commands.SendEmail;

namespace MauiClientApp.Email.EmailEdit.ViewModels;

public partial class EmailEditViewModel(IMediator mediator) : EmailViewModel(mediator, default)
{
    //Properties
    [ObservableProperty]
    public EmailModel emailDraft = null!;

    //Life cycle 
    public override void OnViewModelStarting(CancellationToken token = default)
    {
        base.OnViewModelStarting(token);

        EmailDraft = new() 
        {
            Recipients = [],
            Body = string.Empty,
            Subject = string.Empty,
            SenderName = string.Empty,
            Sender = UserSessionService.CurrentUser.EmailAddress,
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
