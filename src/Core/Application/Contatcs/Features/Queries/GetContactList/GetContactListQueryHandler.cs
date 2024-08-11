namespace Application.Contatcs.Features.Queries.GetContactList;

public class GetContactListQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetContactListQuery, Result<List<ContactDto>>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<ContactDto>>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
    {
        var contactEntityList = await _dbContext.Conctacts.ToListAsync(cancellationToken);
        if (contactEntityList.Count == 0) return Result.Failure<List<ContactDto>>(new Error("ContactListEmpty", "Contact list is empty"));

        var contactModelList = contactEntityList.Select(ContactDto.CreateFromEntity).OrderByDescending(email => email.CreatedAt).ToList();

        return Result.Success(contactModelList);
    }
}
