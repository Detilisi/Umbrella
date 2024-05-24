namespace MauiClientApp.Common.Services;

internal static class UserSessionService
{
    public static bool IsAuthenticated { get; set; } = false;
    public static UserModel CurrentUser { set; get; } = null!;
}
