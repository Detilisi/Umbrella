namespace Application.Common.Base;

public abstract class Model
{
    public int EntityId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
}
