using System;
using System.Collections.Generic;

namespace User.Models
{
    public class Role : Entity<Guid>
    {
        public Role()
        {
            this.Users = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public ICollection<UserRole> Users { get; private set; }
    }
}
