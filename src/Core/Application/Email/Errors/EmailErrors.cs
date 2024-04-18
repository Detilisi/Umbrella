namespace Application.Email.Errorsl;

public static class EmailErrors
{
    public static Error EmailNotFound => new(nameof(EmailNotFound), "Email message not found");
}
