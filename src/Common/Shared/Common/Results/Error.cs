namespace Shared.Common.Results;

public class Error(string value, string description)
{
    //Properties
    public string Value { get; } = value;
    public string Description { get; } = description;

    //Common Errors
    public static readonly Error Crash = new(nameof(Crash), "Application crashed!");
    public static readonly Error Cancelled = new(nameof(Cancelled), "The operation was cancelled");
    public static readonly Error PermissionDenied = new(nameof(PermissionDenied), "The user does not have permission");
    public static readonly Error NoError = new(nameof(NoError), "No error has occured");
    public static readonly Error GenericError = new(nameof(GenericError), "An error has occured");
}
