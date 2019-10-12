using GameBox.Application.Infrastructure;
using System;

namespace GameBox.Application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base(Constants.Common.InvalidCredentials)
        { }
    }
}
