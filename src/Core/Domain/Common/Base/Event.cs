using MediatR;

namespace Domain.Common.Base;

public abstract class Event : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
