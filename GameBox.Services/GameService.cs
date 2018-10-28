using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Games;
using System;
using System.Linq;

namespace GameBox.Services
{
    public class GameService : Service, IGameService
    {
        public GameService(GameBoxDbContext database)
            : base(database)
        {
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

        private bool HasGame(string title)
        {
            return Database.Games.Any(g => g.Title == title);
        }
    }
}