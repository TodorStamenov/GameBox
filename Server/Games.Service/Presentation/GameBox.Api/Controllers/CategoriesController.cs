using GameBox.Application.Categories.Commands.CreateCategory;
using GameBox.Application.Categories.Commands.UpdateCategory;
using GameBox.Application.Categories.Queries.GetAllCategories;
using GameBox.Application.Categories.Queries.GetCategory;
using GameBox.Application.Categories.Queries.GetMenuCategories;
using GameBox.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers;

[Authorize(Roles = Constants.Common.Admin)]
public class CategoriesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriesListViewModel>>> Get()
    {
        return base.Ok(await base.Mediator.Send(new GetAllCategoriesQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return base.Ok(await base.Mediator.Send(new GetCategoryQuery { Id = id }));
    }

    [AllowAnonymous]
    [HttpGet("menu")]
    public async Task<ActionResult<IEnumerable<CategoriesListMenuViewModel>>> Menu()
    {
        return base.Ok(await base.Mediator.Send(new GetMenuCategoriesQuery()));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;

        return base.Ok(await base.Mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
    {
        return base.Ok(await base.Mediator.Send(command));
    }
}
