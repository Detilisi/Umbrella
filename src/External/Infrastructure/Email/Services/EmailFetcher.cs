using Domain.Email.Entities.Enums;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private readonly ImapClient _imapClient = new();
    private readonly IEncryptionService _encryptionService;
    //Properties
    private string EmailAddress { get; set; } = string.Empty;
    public bool IsConnected => _imapClient.IsConnected && _imapClient.IsAuthenticated;

    //Construction
    public EmailFetcher(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;

       //Set up client
        _imapClient.CheckCertificateRevocation = false;
    }

    //Methods
    public async Task<Result> ConnectAsync(string emailAddress, string password, CancellationToken token = default)
    {
        if (IsConnected) return Result.Success();

        EmailAddress = emailAddress;
        var settings = ImapSettings.FindServerSettings(emailAddress);
        if (settings.IsFailure) return Result.Failure(Error.Cancelled);

        //Connect to server
        await _imapClient.ConnectAsync(settings.Value.Server, settings.Value.Port, settings.Value.UseSsl, token);

        //Authenticate user
        var decryptedPassword = _encryptionService.Decrypt(password);
        await _imapClient.AuthenticateAsync(emailAddress, decryptedPassword, token);
        return Result.Success();
    }

    public async Task<Result<List<EmailModel>>> LoadEmailsAsync(CancellationToken token = default)
    {
        //Verify connection
        if (!IsConnected) return Result.Failure<List<EmailModel>>(Error.Cancelled); //No connection error

        //Retrieve messages
        var allMessages = new List<EmailModel>();

        // Select the Inbox folder
        await _imapClient.Inbox.OpenAsync(FolderAccess.ReadOnly, token);

        // Search for all messages in the Inbox
        var uids = await _imapClient.Inbox.SearchAsync(SearchQuery.All, token);

        // Fetch the messages
        foreach (var uid in uids)
        {
            var mimeMessage = await _imapClient.Inbox.GetMessageAsync(uid, token);
            allMessages.Add(ConvertToEmailModel(mimeMessage));
        }

        allMessages.Reverse();

        return Result.Success(allMessages);
    }
    public async Task<Result<List<EmailModel>>> LazyLoadEmailsAsync(int pageSize, int skip, CancellationToken token = default)
    {
        if (!IsConnected) return Result.Failure<List<EmailModel>>(Error.Cancelled); // No connection error

        var allMessages = new List<EmailModel>();

        await _imapClient.Inbox.OpenAsync(FolderAccess.ReadOnly, token);

        var uids = await _imapClient.Inbox.SearchAsync(SearchQuery.All, token);
        for (int i = uids.Count - 1 - skip; i >= Math.Max(0, uids.Count - skip - pageSize); i--)
        {
            var uid = uids[i];
            var mimeMessage = await _imapClient.Inbox.GetMessageAsync(uid, token);
            allMessages.Add(ConvertToEmailModel(mimeMessage));
        }


        return Result.Success(allMessages);
    }

    //Helper methods
    private EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var mailbox = mimeMessage.From.Mailboxes.FirstOrDefault();
        var senderAddress = mailbox?.Address ?? "no-reply@email.com";
        var senderName = string.IsNullOrWhiteSpace(mailbox?.Name) ? senderAddress : mailbox.Name;

        return new EmailModel
        {
            EmailType = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Recipients = [EmailAddress],
            CreatedAt = mimeMessage.Date.DateTime,
            Sender = senderAddress,
            SenderName = senderName,
            Subject = mimeMessage.Subject.ShortText() ?? "No subject",
            Body = mimeMessage.TextBody.ShortText() ?? "No message text."
        };
    }

    //Disposal
    public void Dispose()
    {
        _imapClient.Disconnect(true);
        _imapClient.Dispose();
    }
}
