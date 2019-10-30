using GameBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Application.Contracts.Services
{
    public interface IWishlistService
    {
        Task<Guid> AddGameToWishlistAsync(string username, Guid gameId);

        Task<Guid> RemoveGameFromWishlistAsync(string username, Guid gameId);

        Task<IEnumerable<Game>> GetWishlistGamesAsync(string username);

        Task<IEnumerable<Guid>> ClearGamesFromWishlistAsync(string username);
    }
}
