namespace Domain.User.Exceptions;

public class PasswordTooShortException(int limitValue) : Exception($"Password must be at least {limitValue} characters long.")
{
}
