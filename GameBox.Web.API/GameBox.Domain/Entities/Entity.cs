namespace GameBox.Domain.Entities
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
