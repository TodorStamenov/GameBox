﻿using GameBox.Application.Contracts;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameBox.Persistence
{
    public class GameBoxDbContext : DbContext, IGameBoxDbContext
    {
        public GameBoxDbContext(DbContextOptions<GameBoxDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(GameBoxDbContext).Assembly);
        }
    }
}