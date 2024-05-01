using Domain.Email.Entities.Enums;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private UserModel _currentUser = null!;
    private readonly ImapClient _imapClient = new();
    
    //Properties
    public bool IsConnected => _imapClient.IsConnected && _imapClient.IsAuthenticated;

    //Construction
    public EmailFetcher()
    {
       //Set up client
        _imapClient.CheckCertificateRevocation = false;
    }

    //Methods
    public async Task<Result> ConnectAsync(UserModel userModel, CancellationToken token = default)
    {
        if (IsConnected) Result.Success();

        _currentUser = userModel;
        var settings = ImapSettings.FindServerSettings(userModel.EmailDomain);
        if (settings.IsFailure) return Result.Failure(Error.Cancelled);

        //Connect to server
        await _imapClient.ConnectAsync(settings.Value.Server, settings.Value.Port, settings.Value.UseSsl, token);

        //Authenticate user
        await _imapClient.AuthenticateAsync(userModel.EmailAddress, userModel.EmailPassword, token);
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

    //Helper methods
    private EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var senderName = mimeMessage.From.Mailboxes.Select(x => x.Name).FirstOrDefault();
        var senderAddress = mimeMessage.From.Mailboxes.Select(x => x.Address).FirstOrDefault();
        
        return new EmailModel
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Recipients = [_currentUser.EmailAddress],
            CreatedAt = mimeMessage.Date.DateTime,
            Sender = senderAddress ?? "no-reply@email.com",
            Subject = mimeMessage.Subject.ShortText() ?? "No subject",
            Body = mimeMessage.TextBody.ShortText() ?? "No message text.",
        };
    }

    //Disposal
    public void Dispose()
    {
        _imapClient.Disconnect(true);
        _imapClient.Dispose();
    }
}
