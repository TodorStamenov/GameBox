using GameBox.Data.Configurations;
using GameBox.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBox.Data
{
    public class GameBoxDbContext : DbContext
    {
        public GameBoxDbContext(DbContextOptions<GameBoxDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}