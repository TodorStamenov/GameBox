namespace GameBox.Domain.Entities;

public class Category : Entity<Guid>
{
    public Category()
    {
        this.Games = new HashSet<Game>();
    }

    public string Name { get; set; }

    public ICollection<Game> Games { get; private set; }
}
