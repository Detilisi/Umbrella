using Application.User.Errors;

namespace Application.User.Features.Queries.LoginUser;

public class LoginUserQueryHandler(IApplicationDbContext dbContext, IUserSessionService userSessionService) : IRequestHandler<LoginUserQuery, Result<UserModel>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IUserSessionService _userSessionService = userSessionService;

    //Handle method
    public async Task<Result<UserModel>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _dbContext.Users
            .Where(x => x.EmailAddress.Value == request.EmailAddress).FirstOrDefaultAsync(cancellationToken);
        if (userEntity == null) return Result.Failure<UserModel>(AuthenticationErrors.EmailInvalid);

        //Validate password
        var passwordValid = userEntity.EmailPassword.Value.Equals(request.EmailPassword);
        if (!passwordValid) return Result.Failure<UserModel>(AuthenticationErrors.PasswordInvalid);

        var userModel = UserModel.CreateFromEntity(userEntity);
        _userSessionService.CreateSession(userModel);

        return Result.Success(userModel);
    }
}
