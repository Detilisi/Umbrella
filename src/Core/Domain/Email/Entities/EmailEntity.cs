using Domain.Email.Entities.Enums;
using Domain.Email.Events;
using Domain.Email.ValueObjects;

namespace Domain.Email.Entities;

public class EmailEntity : Entity
{
    //Properties
    public EmailType Type { get; }
    public EmailStatus EmailStatus { get;}
    public EmailBodyText Body { get; }
    public EmailSubjectLine Subject { get; }
    public List<EmailAddress> Recipients { get; }

    //Construction
    private EmailEntity() { }
    private EmailEntity
    (
        int id, 
        List<EmailAddress> recipients, 
        EmailSubjectLine subject, 
        EmailBodyText body
    ) : base(id)
    {
        if (recipients is null || subject is null || body is null) throw new ArgumentNullException();

        Body = body;
        Subject = subject;
        Recipients = recipients;
        Type = EmailType.Email;
        EmailStatus = EmailStatus.UnRead;

        AddDomainEvent(new EmailEntityCreatedEvent(this));
    }

    public static EmailEntity Create
    (
        List<EmailAddress> recipients, 
        EmailSubjectLine subject, 
        EmailBodyText body
    )
    {
        return new EmailEntity(0, recipients, subject, body); ;
    }
}
