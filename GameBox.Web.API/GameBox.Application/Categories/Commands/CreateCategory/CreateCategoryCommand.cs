using GameBox.Application.Contracts;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
        {
            private readonly IGameBoxDbContext context;

            public CreateCategoryCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category
                {
                    Name = request.Name
                };

                await this.context.Categories.AddAsync(entity);
                await this.context.SaveChangesAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(Category), request.Name, Constants.Common.Added);
            }
        }
    }
}
