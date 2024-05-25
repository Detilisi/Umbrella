namespace Application.User.Abstractions;

public interface IUserSessionService
{
    public Result CreateSession(UserModel user, CancellationToken token = default);
    public Result<UserModel> GetCurrentSession(CancellationToken token = default);
    public Result DeleteCurrentSession(CancellationToken token = default);
}
