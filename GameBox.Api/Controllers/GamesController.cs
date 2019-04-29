using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Games;
using GameBox.Services.Models.View.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    public class GamesController : BaseApiController
    {
        private readonly IGameService game;

        public GamesController(IGameService game)
        {
            this.game = game;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ListGamesViewModel> Get([FromQuery]int loadedGames, [FromQuery]Guid? categoryId)
        {
            return this.game.ByCategory(loadedGames, categoryId);
        }

        [HttpGet]
        [Route("details/{id}")]
        public IActionResult Details([FromRoute]Guid id)
        {
            return Ok(this.game.Details(id));
        }

        [HttpGet]
        [Authorize]
        [Route("owned")]
        public IEnumerable<ListGamesViewModel> Owned([FromQuery]int loadedGames)
        {
            return this.game.Owned(loadedGames, User.Identity.Name);
        }

        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("all")]
        public IEnumerable<ListGamesAdminViewModel> Get([FromQuery]string title)
        {
            return this.game.All(title);
        }

        [HttpGet]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public IActionResult Get([FromRoute]Guid id)
        {
            return Ok(game.Get(id));
        }

        [HttpPut]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public IActionResult Put([FromRoute]Guid id, [FromBody]GameBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.game.Edit(
                id,
                model.Title,
                model.Description,
                model.ThumbnailUrl,
                model.Price,
                model.Size,
                model.VideoId,
                model.ReleaseDate,
                model.CategoryId);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Common.Admin)]
        public IActionResult Post([FromBody]GameBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.game.Create(
                model.Title,
                model.Description,
                model.ThumbnailUrl,
                model.Price,
                model.Size,
                model.VideoId,
                model.ReleaseDate,
                model.CategoryId);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = Constants.Common.Admin)]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            ServiceResult result = this.game.Delete(id);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}