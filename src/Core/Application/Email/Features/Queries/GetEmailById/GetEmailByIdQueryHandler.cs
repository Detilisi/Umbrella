using Application.Email.Errorsl;
using Microsoft.EntityFrameworkCore;

namespace Application.Email.Features.Queries.GetEmailById;

public class GetEmailByIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetEmailByIdQuery, Result<EmailModel>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<EmailModel>> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
    {
        var emailEntity = await _dbContext.Emails.Where(x => x.Id == request.EmailId).FirstOrDefaultAsync(cancellationToken);
        if (emailEntity == null) return Result.Failure<EmailModel>(EmailErrors.EmailNotFound);

        var emailModel = EmailModel.CreateFromEntity(emailEntity);
        return Result.Success(emailModel);
    }
}
