using Application.User.Dtos;

namespace Application.User.Abstractions.Services;

public interface IUserSessionService
{
    public Result CreateSession(UserDto user);
    public Result<UserDto> GetCurrentSession();
    public Result DeleteCurrentSession();
}
