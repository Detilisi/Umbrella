namespace Application.User.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidEmailOrPassword => new(nameof(InvalidEmailOrPassword), "The specified email or password are incorrect.");
}
