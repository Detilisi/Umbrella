namespace Domain.Common.Exceptions;

public class EmptyValueException(string valueName) : Exception($"{valueName} cannot be empty or null.")
{
}
