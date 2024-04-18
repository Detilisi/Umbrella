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
    public EmailAddress Sender { get; }
    public List<EmailAddress> Recipients { get; }

    //Construction
    private EmailEntity() { }
    private EmailEntity
    (
        int id,
        EmailAddress sender,
        List<EmailAddress> recipients, 
        EmailSubjectLine subject, 
        EmailBodyText body
    ) : base(id)
    {
        if (sender is null || recipients is null || subject is null || body is null) throw new ArgumentNullException();

        Body = body;
        Subject = subject;
        Sender = sender;
        Recipients = recipients;
        Type = EmailType.Email;
        EmailStatus = EmailStatus.UnRead;

        AddDomainEvent(new EmailEntityCreatedEvent(this));
    }

    public static EmailEntity Create
    (
        EmailAddress sender,
        List<EmailAddress> recipients, 
        EmailSubjectLine subject, 
        EmailBodyText body
    )
    {
        return new EmailEntity(0, sender, recipients, subject, body); ;
    }
}
