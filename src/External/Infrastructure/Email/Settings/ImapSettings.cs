using Infrastructure.Email.Settings.Base;

namespace Infrastructure.Email.Settings;

internal class ImapSettings
{
    //Constants
    public static readonly EmailSettings Gmail = new("Gmail", "imap.gmail.com", 993, true);
    public static readonly EmailSettings Yahoo = new("Yahoo", "imap.mail.yahoo.com", 993, true);
    public static readonly EmailSettings Outlook = new("Outlook", "outlook.office365.com", 993, true);

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
