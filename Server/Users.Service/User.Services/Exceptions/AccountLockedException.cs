using System;

namespace User.Services.Exceptions;

public class AccountLockedException : Exception
{
    public AccountLockedException(string username)
        : base($"User {username} is locked!")
    { }
}
