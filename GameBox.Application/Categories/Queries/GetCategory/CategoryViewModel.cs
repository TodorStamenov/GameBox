using GameBox.Application.Contracts.Mapping;
using GameBox.Domain.Entities;

namespace GameBox.Application.Categories.Queries.GetCategory
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}