using System;

namespace User.Services.Exceptions
{
    public class RoleEditException : Exception
    {
        public RoleEditException(string username, string roleName, bool isInRole)
            : base($"User {username} is {(isInRole ? "already" : "not")} in {roleName} role.")
        { }
    }
}
