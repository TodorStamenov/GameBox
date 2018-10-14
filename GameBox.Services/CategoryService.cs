using GameBox.Data;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Categories;
using System.Collections.Generic;
using System.Linq;

namespace GameBox.Services
{
    public class CategoryService : Service, ICategoryService
    {
        public CategoryService(GameBoxDbContext database)
            : base(database)
        {
        }

        public IEnumerable<ListCategoriesViewModel> All()
        {
            return Database
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new ListCategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Games = c.Games.Count
                })
                .ToList();
        }
    }
}