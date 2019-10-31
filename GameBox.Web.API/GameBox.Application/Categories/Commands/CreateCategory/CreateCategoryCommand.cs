using MediatR;

namespace GameBox.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }
    }
}
