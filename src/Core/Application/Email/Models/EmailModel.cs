namespace Application.Email.Models;

public class EmailModel : Model
{
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    
    public required EmailBodyText Body { get; set; }
    public required EmailSubjectLine Subject { get; set; }

    public required EmailAddress Sender { get; set; }
    public required List<EmailAddress> Recipients { get; set; }
}
