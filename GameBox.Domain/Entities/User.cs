using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class User
    {
        public User()
        {
            this.Roles = new HashSet<UserRoles>();
            this.Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsLocked { get; set; }

        public byte[] Salt { get; set; }

        public ICollection<UserRoles> Roles { get; private set; }

        public ICollection<Order> Orders { get; private set; }
    }
}