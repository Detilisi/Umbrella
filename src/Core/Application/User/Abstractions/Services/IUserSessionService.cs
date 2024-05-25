namespace Application.User.Abstractions.Services;

public interface IUserSessionService
{
    public Result CreateSession(UserModel user, CancellationToken token = default);
    public Result<UserModel> GetCurrentSession(CancellationToken token = default);
    public Result DeleteCurrentSession(CancellationToken token = default);
}
