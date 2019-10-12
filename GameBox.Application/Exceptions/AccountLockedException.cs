using System;

namespace GameBox.Application.Exceptions
{
    public class AccountLockedException : Exception
    {
        public AccountLockedException(string username) 
            : base($"User {username} is locked!")
        { }
    }
}
