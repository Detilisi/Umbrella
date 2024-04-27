using Application.Email.Errors;

namespace Application.Email.Features.Queries.GetEmailList;

public class GetEmailListQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetEmailListQuery, Result<List<EmailModel>>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<List<EmailModel>>> Handle(GetEmailListQuery request, CancellationToken cancellationToken)
    {
        var emailEntityList = await _dbContext.Emails.ToListAsync(cancellationToken);
        if (emailEntityList.Count == 0) return Result.Failure<List<EmailModel>>(EmailErrors.EmailNotFound);
        
        // Use Select to map entities to models directly
        var emailModelList = emailEntityList.Select(EmailModel.CreateFromEntity).ToList();

        return Result.Success(emailModelList);
    }

}
