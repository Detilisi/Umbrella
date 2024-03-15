using Domain.Email.Entities;

namespace Domain.Email.Events;

public class EmailEntityCreatedEvent(EmailEntity emailEntity) : Event
{
    public EmailEntity EventEntity { get; set; } = emailEntity;
}
