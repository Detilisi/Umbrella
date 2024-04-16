namespace Application.User.Errors;

public static class AuthenticationErrors
{
    public static Error InvalidEmailOrPassword => new($"Authentication.{nameof(InvalidEmailOrPassword)}","The specified email or password are incorrect.");
}
