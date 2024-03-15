namespace Domain.Email.Exceptions;

public class EmailBodyTooLongException(int limitValue) : Exception($"Email body content exceeds the maximum length of {limitValue} characters.")
{
}
