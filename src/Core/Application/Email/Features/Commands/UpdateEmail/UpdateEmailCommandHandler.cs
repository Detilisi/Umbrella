namespace Application.Email.Features.Commands.UpdateEmail;

public class UpdateEmailCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateEmailCommand, Result<int>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle methods
    public async Task<Result<int>> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var emailEntity = request.ToEmailEntity();

            _dbContext.Emails.Update(emailEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(emailEntity.Id);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
