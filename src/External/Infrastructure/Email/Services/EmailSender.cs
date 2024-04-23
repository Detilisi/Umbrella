using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.Email.Services;

public class EmailSender : IEmailSender, IDisposable
{
    //Fields
    private readonly SmtpClient _smtpClient = new();

    //Properties
    public bool IsConnected => _smtpClient.IsConnected && _smtpClient.IsAuthenticated;

    //Construction
    public EmailSender()
    {
        //Set up client
        _smtpClient.CheckCertificateRevocation = false;
    }

    //Methods
    public async Task<Result> ConnectAsync(UserModel userModel, CancellationToken token = default)
    {
        if (IsConnected) Result.Success();

        var settings = SmtpSettings.FindServerSettings(userModel.EmailDomain);
        if (settings.IsFailure) return Result.Failure(Error.Cancelled);

        //Connect to server
        await _smtpClient.ConnectAsync(settings.Value.Server, settings.Value.Port, settings.Value.UseSsl, token);

        //Authenticate user
        await _smtpClient.AuthenticateAsync(userModel.EmailAddress, userModel.EmailPassword, token);
        return Result.Success();
    }
    public async Task<Result> SendEmailAsync(EmailModel message, CancellationToken token = default)
    {
        await _smtpClient.SendAsync(ConvertToMime(message), token);
        
        return Result.Success();
    }

    //Helper methods
    private MimeMessage ConvertToMime(EmailModel emailModel)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailModel.Sender, emailModel.Sender)); 
        message.To.AddRange(emailModel.Recipients.Select(recipient => new MailboxAddress(recipient, recipient))); 
        message.Subject = emailModel.Subject;
        message.Body = new TextPart("plain") { Text = emailModel.Body };
        return message;
    }

    //Disposal
    public void Dispose()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }
}
