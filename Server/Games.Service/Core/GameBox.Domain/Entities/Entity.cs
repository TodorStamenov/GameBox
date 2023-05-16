namespace GameBox.Domain.Entities;

public abstract class Entity<TKey>
{
    public TKey Id { get; set; }

    public byte[] TimeStamp { get; set; }
}
