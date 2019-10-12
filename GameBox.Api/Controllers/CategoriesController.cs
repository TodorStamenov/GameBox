using GameBox.Application.Categories.Commands.CreateCategory;
using GameBox.Application.Categories.Commands.UpdateCategory;
using GameBox.Application.Categories.Queries.GetAllCategories;
using GameBox.Application.Categories.Queries.GetCategory;
using GameBox.Application.Categories.Queries.GetMenuCategories;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Api.Controllers
{
    [Authorize(Roles = Constants.Common.Admin)]
    public class CategoriesController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<CategoriesListViewModel>>> Get()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryQuery { Id = id }));
        }

        [HttpGet]
        [Route("menu")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoriesListMenuViewModel>>> Menu()
        {
            return Ok(await Mediator.Send(new GetMenuCategoriesQuery()));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute]Guid id, [FromBody]UpdateCategoryCommand command)
        {
            command.Id = id;

            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}