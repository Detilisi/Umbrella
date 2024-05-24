namespace Application.Email.Features.Commands.SyncInbox;

internal class SyncInboxCommandHandler(IApplicationDbContext dbContext, IEmailFetcher emailFetcher) : IRequestHandler<SyncInboxCommand, Result<int>>
{
    //Fields
    private readonly IEmailFetcher _emailFetcher = emailFetcher;
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<int>> Handle(SyncInboxCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Load emails from server
            var connectResult = await _emailFetcher.ConnectAsync(request, cancellationToken);
            if (connectResult.IsFailure) return Result.Failure<int>(Error.GenericError);

            var loadEmailsResult = await _emailFetcher.LoadEmailsAsync(cancellationToken);
            if (loadEmailsResult.IsFailure) return Result.Failure<int>(Error.GenericError);

            //Save loaded emails to database
            foreach (var emailModel in loadEmailsResult.Value)
            {
                var existingMessage = _dbContext.Emails
                    .FirstOrDefault(m => m.Subject.Value == emailModel.Subject && m.CreatedAt == emailModel.CreatedAt);
                if (existingMessage != null) continue;

                var emailEntity = emailModel.ToEmailEntity();
                _dbContext.Emails.Add(emailEntity);
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(loadEmailsResult.Value.Count);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
