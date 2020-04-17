using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public User()
        {
            this.Roles = new HashSet<UserRoles>();
            this.Orders = new HashSet<Order>();
            this.Wishlist = new HashSet<Wishlist>();
            this.Comments = new HashSet<Comment>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }

        public byte[] Salt { get; set; }

        public ICollection<UserRoles> Roles { get; private set; }

        public ICollection<Order> Orders { get; private set; }

        public ICollection<Wishlist> Wishlist { get; private set; }

        public ICollection<Comment> Comments { get; private set; }
    }
}