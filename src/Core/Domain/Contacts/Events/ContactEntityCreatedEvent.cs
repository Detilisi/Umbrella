using Domain.Contacts.Entities;

namespace Domain.Contacts.Events;

public class ContactEntityCreatedEvent(ContactEntity contactEntity) : Event
{
    public ContactEntity EventEntity { get; set; } = contactEntity;
}
