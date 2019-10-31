﻿using GameBox.Application.Contracts;
using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameBox.Infrastructure
{
    public class WishlistService : IWishlistService
    {
        private readonly IUserService userService;
        private readonly IGameBoxDbContext context;

        public WishlistService(IUserService userService, IGameBoxDbContext context)
        {
            this.userService = userService;
            this.context = context;
        }

        public async Task<Guid> AddGameToWishlistAsync(string username, Guid gameId)
        {
            var userId = await this.userService.GetUserIdAsync(username);

            var gameExists = await this.context
                .Wishlists
                .AnyAsync(w => w.UserId == userId && w.GameId == gameId);

            if (gameExists)
            {
                return gameId;
            }

            var wishlist = new Wishlist
            {
                UserId = userId,
                GameId = gameId
            };

            await this.context.Wishlists.AddAsync(wishlist);
            await this.context.SaveChangesAsync();

            return gameId;
        }

        public async Task<Guid> RemoveGameFromWishlistAsync(string username, Guid gameId)
        {
            var userId = await this.userService.GetUserIdAsync(username);
            var wishlist = await this.context.Wishlists.FindAsync(userId, gameId);

            if (wishlist == null)
            {
                throw new NotFoundException(nameof(Game), gameId);
            }

            this.context.Wishlists.Remove(wishlist);
            await this.context.SaveChangesAsync();

            return gameId;
        }

        public async Task<IEnumerable<Game>> GetWishlistGamesAsync(string username)
        {
            var userId = await this.userService.GetUserIdAsync(username);

            return await this.context
                .Wishlists
                .Where(w => w.UserId == userId)
                .Select(w => w.Game)
                .OrderBy(g => g.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ClearGamesFromWishlistAsync(string username)
        {
            var userId = await this.userService.GetUserIdAsync(username);

            var result = new List<Guid>();

            var games = await this.context
                .Wishlists
                .Where(w => w.UserId == userId)
                .ToListAsync();

            foreach (var game in games)
            {
                this.context.Wishlists.Remove(game);

                result.Add(game.GameId);
            }

            await this.context.SaveChangesAsync();

            return result;
        }
    }
}