namespace Domain.Email.Exceptions;

public class InvalidSubjectException : Exception
{
    public InvalidSubjectException() : base("Subject line cannot contain newlines or carriage returns.")
    {
    }
}