using Application.Email.Dtos;
using MailKit.Net.Smtp;
using MimeKit;

namespace Infrastructure.Email.Services;

public class EmailSender : IEmailSender, IDisposable
{
    //Fields
    private readonly SmtpClient _smtpClient = new();
    private readonly IEncryptionService _encryptionService;

    //Properties
    public bool IsConnected => _smtpClient.IsConnected && _smtpClient.IsAuthenticated;

    //Construction
    public EmailSender(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;

        //Set up client
        _smtpClient.CheckCertificateRevocation = false;
    }

    //Methods
    public async Task<Result> ConnectAsync(string emailAddress, string password, CancellationToken token = default)
    {
        if (IsConnected) Result.Success();

        var settings = SmtpSettings.FindServerSettings(emailAddress);
        if (settings.IsFailure) return Result.Failure(Error.Cancelled);

        //Connect to server
        await _smtpClient.ConnectAsync(settings.Value.Server, settings.Value.Port, settings.Value.UseSsl, token);

        //Authenticate user
        var decryptedPassword = _encryptionService.Decrypt(password);
        await _smtpClient.AuthenticateAsync(emailAddress, decryptedPassword, token);
        return Result.Success();
    }
    public async Task<Result> SendEmailAsync(EmailDto message, CancellationToken token = default)
    {
        await _smtpClient.SendAsync(ConvertToMime(message), token);
        
        return Result.Success();
    }

    //Helper methods
    private MimeMessage ConvertToMime(EmailDto emailModel)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailModel.Sender, emailModel.Sender)); 
        message.To.Add(new MailboxAddress(emailModel.Recipient, emailModel.Recipient)); 
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
