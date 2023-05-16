using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace GameBox.Infrastructure;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(
        IDataService database,
        IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor
            .HttpContext?
            .User?
            .FindFirstValue(Constants.Common.UserIdClaimKey);

        if (userId != null)
        {
            this.UserId = Guid.Parse(userId);
            this.CustomerId = database
                .All<Customer>()
                .Where(c => c.UserId == this.UserId)
                .Select(c => c.Id)
                .FirstOrDefault();
        }
    }

    public Guid UserId { get; }

    public Guid CustomerId { get; }
}
