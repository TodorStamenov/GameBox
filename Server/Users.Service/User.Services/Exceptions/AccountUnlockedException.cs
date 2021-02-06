using System;

namespace User.Services.Exceptions
{
    public class AccountUnlockedException : Exception
    {
        public AccountUnlockedException(string username)
            : base($"User {username} is locked!")
        { }
    }
}
