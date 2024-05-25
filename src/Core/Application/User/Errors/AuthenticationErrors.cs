namespace Application.User.Errors;

public static class AuthenticationErrors
{
    public static Error EmailInvalid=> new(nameof(EmailInvalid), "The specified email is incorrect.");
    public static Error PasswordInvalid => new(nameof(PasswordInvalid), "The specified password is incorrect.");
    public static Error SessionNotAuthenticated => new(nameof(SessionNotAuthenticated), "This session is not authenticated");
}
