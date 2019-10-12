using System;
using System.Collections.Generic;

namespace GameBox.Domain.Entities
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<UserRoles>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserRoles> Users { get; private set; }
    }
}