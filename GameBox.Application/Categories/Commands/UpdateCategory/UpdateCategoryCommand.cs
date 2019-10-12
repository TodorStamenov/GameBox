using MediatR;
using System;

namespace GameBox.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}