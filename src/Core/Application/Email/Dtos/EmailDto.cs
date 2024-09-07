using Domain.Email.Enums;

namespace Application.Email.Dtos;

public class EmailDto : Dto
{
    //Properties
    public EmailType EmailType { get; set; }
    public EmailStatus EmailStatus { get; set; }

    public required string Body { get; set; }
    public required string Subject { get; set; }

    public required string Sender { get; set; }
    public required string SenderName { get; set; }
    public required string Recipient { get; set; }

    //Methods
    internal static EmailDto CreateFromEntity(EmailEntity emailEntity)
    {
        return new EmailDto
        {
            EntityId = emailEntity.Id,
            CreatedAt = emailEntity.CreatedAt,
            ModifiedAt = emailEntity.ModifiedAt,
            EmailType = emailEntity.Type,
            EmailStatus = emailEntity.EmailStatus,
            Body = emailEntity.Body.Value,
            Subject = emailEntity.Subject.Value,
            Sender = emailEntity.Sender.Value,
            SenderName = emailEntity.SenderName,
            Recipient = emailEntity.Recipient.Value,
        };
    }

    internal EmailEntity ToEmailEntity()
    {
        var emailEntity = EmailEntity.Create(
            EmailAddress.Create(Sender),
            EmailAddress.Create(Recipient),
            EmailSubjectLine.Create(Subject),
            EmailBodyText.Create(Body)
        );

        emailEntity.SenderName = SenderName;
        emailEntity.CreatedAt = CreatedAt;
        emailEntity.ModifiedAt = ModifiedAt;

        return emailEntity;
    }

}
