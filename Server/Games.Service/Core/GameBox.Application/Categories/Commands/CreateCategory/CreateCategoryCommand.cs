using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;

namespace GameBox.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<string>
{
    public string Name { get; set; }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly IDataService context;

        public CreateCategoryCommandHandler(IDataService context)
        {
            this.context = context;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            await this.context.AddAsync(category);
            await this.context.SaveAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Category), request.Name, Constants.Common.Added);
        }
    }
}
