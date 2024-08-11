using Application.User.Dtos;
using Application.User.Errors;

namespace Application.User.Features.Queries.LoginUser;

public class LoginUserQueryHandler(IApplicationDbContext dbContext, IUserSessionService userSessionService) : IRequestHandler<LoginUserQuery, Result<UserDto>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IUserSessionService _userSessionService = userSessionService;

    //Handle method
    public async Task<Result<UserDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _dbContext.Users
            .Where(x => x.EmailAddress.Value == request.EmailAddress).FirstOrDefaultAsync(cancellationToken);
        if (userEntity == null) return Result.Failure<UserDto>(AuthenticationErrors.EmailInvalid);

        //Validate password
        var passwordValid = userEntity.EmailPassword.Value.Equals(request.EmailPassword);
        if (!passwordValid) return Result.Failure<UserDto>(AuthenticationErrors.PasswordInvalid);

        var userModel = UserDto.CreateFromEntity(userEntity);
        _userSessionService.CreateSession(userModel);

        return Result.Success(userModel);
    }
}
