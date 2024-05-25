namespace Application.Email.Features.Commands.SendEmail;

internal class SendEmailCommandHandler(IUserSessionService userSessionService, IEmailSender emailSender) : IRequestHandler<SendEmailCommand, Result<int>>
{
    //Fields
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IUserSessionService _userSessionService = userSessionService;

    //Handle methods
    public async Task<Result<int>> Handle(SendEmailCommand request, CancellationToken token)
    {
        try
        {
            var currentUserResult = _userSessionService.GetCurrentSession();
            if (currentUserResult.IsFailure) Result.Failure<int>(currentUserResult.Error);

            var currentUser = currentUserResult.Value;
            var connectResult = await _emailSender.ConnectAsync(currentUser.EmailAddress, currentUser.EmailPassword, token);
            if (connectResult.IsFailure) return Result.Failure<int>(connectResult.Error);

            var sendResult = await _emailSender.SendEmailAsync(request.EmailMessage, token);
            if (sendResult.IsFailure) return Result.Failure<int>(sendResult.Error);

            return Result.Success(request.EmailMessage.EntityId);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }

    }
}
