using Application.Email.Dtos;
using Application.Email.Errors;

namespace Application.Email.Features.Queries.GetEmailById;

public class GetEmailByIdQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetEmailByIdQuery, Result<EmailDto>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<EmailDto>> Handle(GetEmailByIdQuery request, CancellationToken cancellationToken)
    {
        var emailEntity = await _dbContext.Emails.Where(x => x.Id == request.EmailId).FirstOrDefaultAsync(cancellationToken);
        if (emailEntity == null) return Result.Failure<EmailDto>(EmailErrors.EmailNotFound);

        var emailModel = EmailDto.CreateFromEntity(emailEntity);
        return Result.Success(emailModel);
    }
}
