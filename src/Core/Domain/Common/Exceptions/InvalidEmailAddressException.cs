namespace Domain.Common.Exceptions;

public class InvalidEmailAddressException : Exception
{
    public InvalidEmailAddressException() : base("Invalid email address format.")
    {
    }
}