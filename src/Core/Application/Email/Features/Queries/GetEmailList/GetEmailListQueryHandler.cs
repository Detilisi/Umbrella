using Application.Email.Dtos;
using Application.Email.Errors;

namespace Application.Email.Features.Queries.GetEmailList;

public class GetEmailListQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetEmailListQuery, Result<List<EmailDto>>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<List<EmailDto>>> Handle(GetEmailListQuery request, CancellationToken cancellationToken)
    {
        var emailEntityList = await _dbContext.Emails.ToListAsync(cancellationToken);
        if (emailEntityList.Count == 0) return Result.Failure<List<EmailDto>>(EmailErrors.EmailNotFound);
        
        // Use Select to map entities to models directly
        var emailModelList = emailEntityList.Select(EmailDto.CreateFromEntity).OrderByDescending(email => email.CreatedAt).ToList();

        return Result.Success(emailModelList);
    }
}
