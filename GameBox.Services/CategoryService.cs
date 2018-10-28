using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Data;
using GameBox.Data.Models;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Categories;
using GameBox.Services.Models.View.Categories;
using System;
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

        public ServiceResult Create(string name)
        {
            if (this.HasCategory(name))
            {
                return GetServiceResult(string.Format(Constants.Common.DuplicateEntry, nameof(Category), name), ServiceResultType.Fail);
            }

            Database.Categories.Add(new Category
            {
                Name = name,
            });

            Database.SaveChanges();

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(Category), name, Constants.Common.Added), ServiceResultType.Success);
        }

        public ServiceResult Edit(Guid id, string name)
        {
            Category category = Database.Categories.Find(id);

            if (category == null)
            {
                return GetServiceResult(string.Format(Constants.Common.NotExistingEntry, nameof(Category), name), ServiceResultType.Fail);
            }

            if (this.HasCategory(name) && category.Name != name)
            {
                return GetServiceResult(string.Format(Constants.Common.DuplicateEntry, nameof(Category), name), ServiceResultType.Fail);
            }

            category.Name = name;
            Database.SaveChanges();

            return GetServiceResult(string.Format(Constants.Common.Success, nameof(Category), name, Constants.Common.Edited), ServiceResultType.Success);
        }

        public CategoryBindingModel Get(Guid id)
        {
            return Database
                .Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryBindingModel
                {
                    Name = c.Name
                })
                .FirstOrDefault();
        }

        public IEnumerable<ListMenuCategoriesViewModel> Menu()
        {
            return Database
                .Categories
                .Where(c => c.Games.Any())
                .OrderBy(c => c.Name)
                .Select(c => new ListMenuCategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
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

        private bool HasCategory(string name)
        {
            return this.Database.Categories.Any(c => c.Name == name);
        }
    }
}