using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<string>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
    {
        private readonly IDataService context;

        public UpdateCategoryCommandHandler(IDataService context)
        {
            this.context = context;
        }

        public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = this.context
                .All<Category>()
                .Where(c => c.Id == request.Id)
                .FirstOrDefault();

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.Name = request.Name;

            await this.context.SaveAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Category), request.Name, Constants.Common.Edited);
        }
    }
}
