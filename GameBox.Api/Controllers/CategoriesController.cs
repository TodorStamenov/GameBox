using GameBox.Core;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Route("all")]
        public IEnumerable<ListCategoriesViewModel> All()
        {
            return this.category.All();
        }
    }
}