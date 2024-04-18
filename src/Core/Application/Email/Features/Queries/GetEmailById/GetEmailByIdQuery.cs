namespace Application.Email.Features.Queries.GetEmailById;

public class GetEmailByIdQuery(int userId, int emailId) : IRequest<Result<EmailModel>>
{
    public int UserId { get; } = userId;
    public int EmailId { get; } = emailId;
}

