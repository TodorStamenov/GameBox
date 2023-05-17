using User.Services.Infrastructure;

namespace User.Services.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base(Constants.Common.InvalidCredentials)
    { }
}
