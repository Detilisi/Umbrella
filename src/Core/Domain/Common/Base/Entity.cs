using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Base;

public abstract class Entity
{
    //Fields
    private readonly List<Event> _domainEvents = new();
    [NotMapped] public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();

    //Properties
    public int Id { get; protected set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    //Construction
    public Entity()
    {
        //Required for Entity framework
    }
    public Entity(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    //Event methods
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void AddDomainEvent(Event domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(Event domainEvent) => _domainEvents.Remove(domainEvent);
    
    //Overide methods
    public override int GetHashCode() => Id.GetHashCode();
    public override bool Equals(object? obj) { return obj is Entity entity && Id.Equals(entity.Id); }
}
