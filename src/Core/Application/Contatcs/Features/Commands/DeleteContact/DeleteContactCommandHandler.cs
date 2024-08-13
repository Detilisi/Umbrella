namespace Application.Contatcs.Features.Commands.DeleteContact;

public class DeleteContactCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteContactCommand, Result<int>>
{
    public async Task<Result<int>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var contactEntity = ContactEntity.Create
            (
                request.Name,
                EmailAddress.Create(request.EmailAddress)
            );

            dbContext.Conctacts.Remove(contactEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(contactEntity.Id);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
