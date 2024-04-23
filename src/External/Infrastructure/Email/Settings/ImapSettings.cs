using Infrastructure.Email.Settings.Base;

namespace Infrastructure.Email.Settings;

internal class ImapSettings(string provider, string server, int port, bool useSsl) : EmailSettings(provider, server, port, useSsl)
{
    //Constants
    public static readonly ImapSettings Gmail = new("Gmail", "imap.gmail.com", 993, true);
    public static readonly ImapSettings Yahoo = new("Yahoo", "imap.mail.yahoo.com", 993, true);
    public static readonly ImapSettings Outlook = new("Outlook", "outlook.office365.com", 993, true);

    //Helper methods
    public static Result<ImapSettings> FindServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => Result.Success(Gmail),
            "yahoo.com" => Result.Success(Yahoo),
            "outlook.com" or "office365.com" => Result.Success(Outlook),
            _ => Result.Failure<ImapSettings>(new Error("EmailDomainUnsupported", "Email provider for domain '{emailDomain}' is not supported"))
        };
    }
}
