using System;

namespace GameBox.Application.Exceptions
{
    public class AccountUnlockedException : Exception
    {
        public AccountUnlockedException(string username)
            : base($"User {username} is locked!")
        { }
    }
}
