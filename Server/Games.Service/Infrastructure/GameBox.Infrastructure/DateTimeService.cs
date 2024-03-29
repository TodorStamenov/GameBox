﻿using GameBox.Application.Contracts.Services;

namespace GameBox.Infrastructure;

public class DateTimeService : IDateTimeService
{
    public DateTime Now
    {
        get { return DateTime.Now; }
    }

    public DateTime UtcNow
    {
        get { return DateTime.UtcNow; }
    }
}
