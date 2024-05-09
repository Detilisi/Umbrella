namespace Application.Email.Features.Commands.CreateEmail;

public class CreateEmailCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateEmailCommand, Result<int>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle methods
    public async Task<Result<int>> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var emailEntity = request.ToEmailEntity();

            _dbContext.Emails.Add(emailEntity);
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
