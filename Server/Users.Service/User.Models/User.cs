using System;
using System.Collections.Generic;

namespace User.Models;

public class User : Entity<Guid>
{
    public User()
    {
        this.Roles = new HashSet<UserRole>();
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsLocked { get; set; }

    public byte[] Salt { get; set; }

    public ICollection<UserRole> Roles { get; private set; }
}
