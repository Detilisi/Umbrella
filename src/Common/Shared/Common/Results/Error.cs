namespace Shared.Common.Results;

public class Error(string code, string message)
{
    //Properties
    public string Code { get; } = code;
    public string Message { get; } = message;

    //Operators
    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    //Common Errors
    public static readonly Error Crash = new(nameof(Crash), "Application crashed!");
    public static readonly Error Cancelled = new(nameof(Cancelled), "The operation was cancelled");
    public static readonly Error PermissionDenied = new(nameof(PermissionDenied), "The user does not have permission");
    public static readonly Error None = new(nameof(None), "No error has occured");
    public static readonly Error GenericError = new(nameof(GenericError), "An error has occured");
}
