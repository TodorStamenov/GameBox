using GameBox.Core;
using GameBox.Services.Models.Binding.Games;
using GameBox.Services.Models.View.Game;
using System;
using System.Collections.Generic;

namespace GameBox.Services.Contracts
{
    public interface IGameService
    {
        GameDetailsViewModel Details(Guid id);

        GameBindingModel Get(Guid id);

        ServiceResult Create(
            string title,
            string description,
            string thumbnailUrl,
            decimal price,
            double size,
            string videoId,
            string releaseDateString,
            Guid categoryId);

        ServiceResult Edit(
            Guid id,
            string title,
            string description,
            string thumbnailUrl,
            decimal price,
            double size,
            string videoId,
            string releaseDateString,
            Guid categoryId);

        ServiceResult Delete(Guid id);

        IEnumerable<ListGamesCartViewModel> Cart(IEnumerable<Guid> gameIds);

        IEnumerable<ListGamesViewModel> ByCategory(int loadedGames, Guid? categoryId);

        IEnumerable<ListGamesViewModel> Owned(int loadedGames, string name);
    }
}