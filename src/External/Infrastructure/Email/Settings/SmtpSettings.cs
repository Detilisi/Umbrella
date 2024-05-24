using Infrastructure.Email.Settings.Base;

namespace Infrastructure.Email.Settings;

internal class SmtpSettings
{
    //Constants
    public static readonly EmailSettings Gmail = new("Gmail", "smtp.gmail.com", 465, true);
    public static readonly EmailSettings Yahoo = new("Yahoo", "smtp.mail.yahoo.com", 587, true);
    public static readonly EmailSettings Outlook = new("Outlook", "smtp.office365.com", 587, true);

    //Helper methods
    public static Result<EmailSettings> FindServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => Result.Success(Gmail),
            "yahoo.com" => Result.Success(Yahoo),
            "outlook.com" or "office365.com" => Result.Success(Outlook),
            _ => Result.Failure<EmailSettings>(new Error("EmailDomainUnsupported", "Email provider for domain '{emailDomain}' is not supported"))
        };
    }
}

