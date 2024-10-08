﻿namespace Application.Contatcs.Features.Commands.DeleteContact;

public class DeleteContactCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteContactCommand, Result<int>>
{
    public async Task<Result<int>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = dbContext.Conctacts.Find(request.EntityId);
            if (entity == null) return Result.Failure<int>(Error.NotFoundError);

            dbContext.Conctacts.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(request.EntityId);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
    }
}
