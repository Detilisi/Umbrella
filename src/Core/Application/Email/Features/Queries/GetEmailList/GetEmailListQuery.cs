using Application.Email.Dtos;

namespace Application.Email.Features.Queries.GetEmailList;

public class GetEmailListQuery() : IRequest<Result<List<EmailDto>>> { }
