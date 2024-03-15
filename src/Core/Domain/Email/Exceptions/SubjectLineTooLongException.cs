namespace Domain.Email.Exceptions;

public class SubjectLineTooLongException(int limitValue) : Exception($"Subject line exceeds the maximum length of {limitValue} characters.")
{
}
