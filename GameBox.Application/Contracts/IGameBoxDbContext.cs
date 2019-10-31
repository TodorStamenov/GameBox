using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Contracts
{
    public interface IGameBoxDbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Role> Roles { get; set; }

        DbSet<UserRoles> UserRoles { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<Wishlist> Wishlists { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task SeedAsync();
    }
}
