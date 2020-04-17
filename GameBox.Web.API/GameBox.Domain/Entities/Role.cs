using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class Role : Entity<Guid>
    {
        public Role()
        {
            this.Users = new HashSet<UserRoles>();
        }

        public string Name { get; set; }

        public ICollection<UserRoles> Users { get; private set; }
    }
}