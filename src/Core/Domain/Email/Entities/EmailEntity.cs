using Domain.Email.Enums;
using Domain.Email.Events;
using Domain.Email.ValueObjects;

namespace Domain.Email.Entities;

public class EmailEntity : Entity
{
    //Properties
    public EmailType Type { get; }
    public EmailStatus EmailStatus { get; }
    public EmailBodyText Body { get; }
    public EmailSubjectLine Subject { get; }
    public EmailAddress Sender { get; }
    public string SenderName { get; set; }
    public EmailAddress Recipient { get; }

    //Construction
    private EmailEntity() { }
    private EmailEntity
    (
        int id,
        EmailAddress sender,
        EmailAddress recipient,
        EmailSubjectLine subject,
        EmailBodyText body
    ) : base(id)
    {
        if (sender is null || recipient is null || subject is null || body is null) throw new ArgumentNullException();

        Body = body;
        Subject = subject;
        Sender = sender;
        Recipient = recipient;
        Type = EmailType.Email;
        EmailStatus = EmailStatus.UnRead;

        SenderName ??= sender.Value;

        AddDomainEvent(new EmailEntityCreatedEvent(this));
    }

    public static EmailEntity Create
    (
        EmailAddress sender,
        EmailAddress recipient,
        EmailSubjectLine subject,
        EmailBodyText body
    )
    {
        return new EmailEntity(0, sender, recipient, subject, body); ;
    }
}
