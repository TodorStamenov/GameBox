using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Games;
using GameBox.Services.Models.View.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBox.Services
{
    public class GameService : Service, IGameService
    {
        public GameService(GameBoxDbContext database)
            : base(database)
        {
        }

        public GameDetailsViewModel Details(Guid id)
        {
            Game game = Database.Games.Find(id);

            if (game == null)
            {
                return new GameDetailsViewModel();
            }

            game.ViewCount++;

            Database.SaveChanges();

            return new GameDetailsViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Price = game.Price,
                Size = game.Size,
                VideoId = game.VideoId,
                ThumbnailUrl = game.ThumbnailUrl,
                Description = game.Description,
                ViewCount = game.ViewCount,
                ReleaseDate = game.ReleaseDate.ToShortDateString()
            };
        }

        public GameBindingModel Get(Guid id)
        {
            return Database
                .Games
                .Where(g => g.Id == id)
                .Select(g => new GameBindingModel
                {
                    Title = g.Title,
                    Description = g.Description,
                    ThumbnailUrl = g.ThumbnailUrl,
                    Price = g.Price,
                    Size = g.Size,
                    VideoId = g.VideoId,
                    ReleaseDate = g.ReleaseDate.Date.ToString(),
                    CategoryId = g.CategoryId
                })
                .FirstOrDefault();
        }

        public ServiceResult Create(
            string title,
            string description,
            string thumbnailUrl,
            decimal price,
            double size,
            string videoId,
            string releaseDateString,
            Guid categoryId)
        {
            if (!DateTime.TryParse(releaseDateString, out DateTime releaseDate))
            {
                return GetServiceResult(string.Format(Constants.Common.NotValidDateFormat, releaseDateString), ServiceResultType.Fail);
            }

            if (this.HasGame(title))
            {
                return GetServiceResult(string.Format(Constants.Common.DuplicateEntry, nameof(Game), title), ServiceResultType.Fail);
            }

            Database.Games.Add(new Game
            {
                Title = title,
                Description = description,
                ThumbnailUrl = thumbnailUrl,
                Price = price,
                Size = size,
                VideoId = videoId,
                ReleaseDate = releaseDate,
                CategoryId = categoryId
            });

            Database.SaveChanges();

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(Game), title, Constants.Common.Added), ServiceResultType.Success);
        }

        public ServiceResult Edit(
            Guid id,
            string title,
            string description,
            string thumbnailUrl,
            decimal price,
            double size,
            string videoId,
            string releaseDateString,
            Guid categoryId)
        {
            if (!DateTime.TryParse(releaseDateString, out DateTime releaseDate))
            {
                return GetServiceResult(string.Format(Constants.Common.NotValidDateFormat, releaseDateString), ServiceResultType.Fail);
            }

            Game game = Database.Games.Find(id);

            if (game == null)
            {
                return GetServiceResult(string.Format(Constants.Common.NotExistingEntry, nameof(Game), title), ServiceResultType.Fail);
            }

            if (this.HasGame(title) && game.Title != title)
            {
                return GetServiceResult(string.Format(Constants.Common.DuplicateEntry, nameof(Game), title), ServiceResultType.Fail);
            }

            game.Title = title;
            game.Description = description;
            game.ThumbnailUrl = thumbnailUrl;
            game.Price = price;
            game.Size = size;
            game.VideoId = videoId;
            game.ReleaseDate = releaseDate;
            game.CategoryId = categoryId;

            Database.SaveChanges();

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(Game), title, Constants.Common.Edited), ServiceResultType.Success);
        }

        public ServiceResult Delete(Guid id)
        {
            Game game = Database.Games.Find(id);

            if (game == null)
            {
                return GetServiceResult(string.Format(Constants.Common.NotExistingEntry, nameof(Game), id), ServiceResultType.Fail);
            }

            Database.Games.Remove(game);
            Database.SaveChanges();

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(Game), game.Title, Constants.Common.Deleted), ServiceResultType.Success);
        }

        public IEnumerable<ListGamesViewModel> Get(int loadedGames, Guid? categoryId)
        {
            return Database
                .Games
                .Where(g => categoryId.HasValue ? g.CategoryId == categoryId : true)
                .OrderByDescending(g => g.ReleaseDate)
                .ThenByDescending(g => g.ViewCount)
                .ThenBy(g => g.Title)
                .Skip(loadedGames)
                .Take(9)
                .Select(g => new ListGamesViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    Size = g.Size,
                    VideoId = g.VideoId,
                    ThumbnailUrl = g.ThumbnailUrl,
                    Description = g.Description,
                    ViewCount = g.ViewCount
                })
                .ToList();
        }

        public IEnumerable<ListGamesViewModel> Owned(int loadedGames, string name)
        {
            return Database
                .Users
                .Where(u => u.Username == name)
                .SelectMany(u => u.Orders.SelectMany(o => o.Games.Select(g => g.Game)))
                .Distinct()
                .OrderBy(g => g.Title)
                .Skip(loadedGames)
                .Take(9)
                .Select(g => new ListGamesViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Price = g.Price,
                    Size = g.Size,
                    VideoId = g.VideoId,
                    ThumbnailUrl = g.ThumbnailUrl,
                    Description = g.Description,
                    ViewCount = g.ViewCount
                })
                .ToList();
        }

        private bool HasGame(string title)
        {
            return Database.Games.Any(g => g.Title == title);
        }
    }
}