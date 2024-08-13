namespace Application.Contatcs.Features.Commands.SaveContact;

public class SaveContactCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<SaveContactCommand, Result<int>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<int>> Handle(SaveContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Conctacts.Add(request.ToContactEntity());
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(request.EntityId);
        }
        catch (Exception ex) 
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
