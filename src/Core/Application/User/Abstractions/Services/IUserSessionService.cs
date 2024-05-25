namespace Application.User.Abstractions.Services;

public interface IUserSessionService
{
    public Result CreateSession(UserModel user);
    public Result<UserModel> GetCurrentSession();
    public Result DeleteCurrentSession();
}
