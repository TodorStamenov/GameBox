using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Category;
using GameBox.Services.Models.View.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    [Authorize(Roles = Constants.Common.Admin)]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService category;

        public CategoriesController(ICategoryService category)
        {
            this.category = category;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ListCategoriesViewModel> Get()
        {
            return this.category.All();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute]Guid id)
        {
            return Ok(this.category.Get(id));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute]Guid id, [FromBody]CategoryBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.category.Edit(id, model.Name);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CategoryBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.category.Create(model.Name);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}