namespace GameBox.Application.Contracts.Services;

public interface ICurrentUserService : ITransientService
{
    Guid UserId { get; }

    Guid CustomerId { get; }
}
