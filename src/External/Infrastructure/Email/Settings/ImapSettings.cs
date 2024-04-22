using Infrastructure.Email.Settings.Base;

namespace Infrastructure.Email.Settings;

internal class ImapSettings(string provider, string server, int port, bool useSsl) : EmailSettings(provider, server, port, useSsl)
{
    //Constants
    public static readonly ImapSettings Gmail = new("Gmail", "imap.gmail.com", 993, true);
    public static readonly ImapSettings Yahoo = new("Yahoo", "imap.mail.yahoo.com", 993, true);
    public static readonly ImapSettings Outlook = new("Outlook", "outlook.office365.com", 993, true);

    //Helper methods
    public static ImapSettings FindServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => Gmail,
            "yahoo.com" => Yahoo,
            "outlook.com" or "office365.com" => Outlook,
            _ => throw new NotSupportedException($"Email provider for domain '{emailDomain}' is not supported."),
        };
    }
}
