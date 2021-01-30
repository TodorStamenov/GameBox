namespace User.Models
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
