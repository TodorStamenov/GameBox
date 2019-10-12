using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;
using System;

namespace GameBox.Application.Categories.Queries.GetMenuCategories
{
    public class CategoriesListMenuViewModel : IMapFrom<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}