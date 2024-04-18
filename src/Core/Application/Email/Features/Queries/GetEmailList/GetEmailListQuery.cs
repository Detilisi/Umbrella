namespace Application.Email.Features.Queries.GetEmailList;

public class GetEmailListQuery(int userId) : IRequest<Result<EmailModel>>
{
    public int UserId { get; } = userId;
}
