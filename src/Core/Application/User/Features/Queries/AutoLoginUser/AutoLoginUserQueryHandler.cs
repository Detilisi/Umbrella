using Application.User.Dtos;

namespace Application.User.Features.Queries.AutoLoginUser;

public class AutoLoginUserQueryHandler(IApplicationDbContext dbContext, IUserSessionService userSessionService) : IRequestHandler<AutoLoginUserQuery, Result>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IUserSessionService _userSessionService = userSessionService;

    public async Task<Result> Handle(AutoLoginUserQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(cancellationToken);
        if (userEntity == null) return Result.Failure(new Error("No registered user", ""));

        var userModel = UserDto.CreateFromEntity(userEntity);
        _userSessionService.CreateSession(userModel);

        return Result.Success();
    }
}
