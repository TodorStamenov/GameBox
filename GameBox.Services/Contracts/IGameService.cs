using GameBox.Core;
using GameBox.Services.Models.Binding.Games;
using System;

namespace GameBox.Services.Contracts
{
    public interface IGameService
    {
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
    }
}