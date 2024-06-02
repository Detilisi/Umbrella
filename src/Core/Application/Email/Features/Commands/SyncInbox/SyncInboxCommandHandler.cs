namespace Application.Email.Features.Commands.SyncInbox;

internal class SyncInboxCommandHandler(IApplicationDbContext dbContext, IUserSessionService userSessionService, IEmailFetcher emailFetcher) : IRequestHandler<SyncInboxCommand, Result<int>>
{
    //Fields
    private readonly IEmailFetcher _emailFetcher = emailFetcher;
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IUserSessionService _userSessionService = userSessionService;

    public async Task<Result<int>> Handle(SyncInboxCommand request, CancellationToken token)
    {
        try
        {
            //Load emails from server
            var currentUserResult = _userSessionService.GetCurrentSession();
            if(currentUserResult.IsFailure) return Result.Failure<int>(currentUserResult.Error);

            var currentUser = currentUserResult.Value;
            var connectResult = await _emailFetcher.ConnectAsync(currentUser.EmailAddress, currentUser.EncrytedPassword, token);
            if (connectResult.IsFailure) return Result.Failure<int>(connectResult.Error);

            var loadEmailsResult = await _emailFetcher.LoadEmailsAsync(token);
            if (loadEmailsResult.IsFailure) return Result.Failure<int>(loadEmailsResult.Error);

            _emailFetcher.Dispose();

            //Save loaded emails to database
            foreach (var emailModel in loadEmailsResult.Value)
            {
                var existingMessage = _dbContext.Emails
                    .FirstOrDefault(m => m.Subject.Value == emailModel.Subject && m.CreatedAt == emailModel.CreatedAt);
                if (existingMessage != null) continue;

                var emailEntity = emailModel.ToEmailEntity();
                _dbContext.Emails.Add(emailEntity);
            }
            
            await _dbContext.SaveChangesAsync(token);

            return Result.Success(loadEmailsResult.Value.Count);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
