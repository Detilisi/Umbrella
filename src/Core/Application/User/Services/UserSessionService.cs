using Application.User.Abstractions.Services;
using Application.User.Errors;

namespace Application.User.Services;

public class UserSessionService : IUserSessionService
{
    private static bool _isAuthenticated;
    private static UserModel? _currentUser;

    public Result CreateSession(UserModel currentUser)
    {
        if (_isAuthenticated) return Result.Success();

        _isAuthenticated = true;
        _currentUser = currentUser;

        return Result.Success();
    }

    public Result DeleteCurrentSession()
    {
        if (!_isAuthenticated) return Result.Success();

        _isAuthenticated = false;
        _currentUser = null;

        return Result.Success();
    }

    public Result<UserModel> GetCurrentSession()
    {
        if (!_isAuthenticated || _currentUser is null)
        {
            return Result.Failure<UserModel>(AuthenticationErrors.SessionNotAuthenticated);
        }

        return Result.Success(_currentUser);
    }
}

