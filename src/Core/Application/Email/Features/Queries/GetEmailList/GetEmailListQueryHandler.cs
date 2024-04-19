using Application.Email.Errorsl;
using Microsoft.EntityFrameworkCore;

namespace Application.Email.Features.Queries.GetEmailList;

public class GetEmailListQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetEmailListQuery, Result<List<EmailModel>>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<List<EmailModel>>> Handle(GetEmailListQuery request, CancellationToken cancellationToken)
    {
        var emailEntityList = await _dbContext.Emails.ToListAsync(cancellationToken);
        if (emailEntityList == null|| emailEntityList.Count == 0) return Result.Failure<List<EmailModel>>(EmailErrors.EmailNotFound);

        var emailModelList = new List<EmailModel>();
        foreach (var emailEntity in emailEntityList)
        {
            emailModelList.Add(EmailModel.CreateFromEntity(emailEntity));
        }

        return Result.Success(emailModelList);
    }
}
