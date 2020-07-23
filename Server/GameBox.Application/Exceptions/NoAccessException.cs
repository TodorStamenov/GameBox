using System;

namespace GameBox.Application.Exceptions
{
    public class NoAccessException : Exception
    {
        public NoAccessException()
            : base("You don't have access to permit this operation!")
        { }
    }
}