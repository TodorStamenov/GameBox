using System;

namespace GameBox.Application.Contracts.Services
{
    public interface IDateTimeService : ITransientService
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}
