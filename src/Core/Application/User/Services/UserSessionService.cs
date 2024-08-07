using Application.User.Dtos;
using Application.User.Errors;

namespace Application.User.Services;

public class UserSessionService : IUserSessionService
{
    private static bool _isAuthenticated;
    private static UserDto? _currentUser;

    public Result CreateSession(UserDto currentUser)
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

    public Result<UserDto> GetCurrentSession()
    {
        if (!_isAuthenticated || _currentUser is null)
        {
            return Result.Failure<UserDto>(AuthenticationErrors.SessionNotAuthenticated);
        }

        return Result.Success(_currentUser);
    }
}

