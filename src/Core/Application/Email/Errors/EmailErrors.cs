namespace Application.Email.Errors;

public static class EmailErrors
{
    public static Error EmailNotFound => new(nameof(EmailNotFound), "Email message not found");
}
