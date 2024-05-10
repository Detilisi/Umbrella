namespace Application.Email.Models;

public class EmailModel : Model
{
    //Properties
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    
    public required string Body { get; set; }
    public required string Subject { get; set; }

    public required string Sender { get; set; }
    public required string SenderName { get; set; }
    public List<string> Recipients { get; set; } = [];

    //Methods
    public static EmailModel CreateFromEntity(EmailEntity emailEntity)
    {
        return new EmailModel
        {
            EntityId = emailEntity.Id,
            CreatedAt = emailEntity.CreatedAt,
            ModifiedAt = emailEntity.ModifiedAt,
            Type = emailEntity.Type,
            EmailStatus = emailEntity.EmailStatus,
            Body = emailEntity.Body.Value,
            Subject = emailEntity.Subject.Value,
            Sender = emailEntity.Sender.Value,
            SenderName = emailEntity.SenderName,
            Recipients = emailEntity.Recipients.Select(r => r.Value).ToList(),
        };
    }

    public EmailEntity ToEmailEntity()
    {
        var emailEntity = EmailEntity.Create(
            EmailAddress.Create(Sender),
            Recipients.Select(EmailAddress.Create).ToList(),
            EmailSubjectLine.Create(Subject),
            EmailBodyText.Create(Body)
        );

        emailEntity.SenderName = SenderName;
        emailEntity.CreatedAt = CreatedAt;
        emailEntity.ModifiedAt = ModifiedAt;

        return emailEntity;
    }

}
