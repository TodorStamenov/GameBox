using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
        {
            private readonly IGameBoxDbContext context;

            public UpdateCategoryCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await this.context.Set<Category>().FindAsync(request.Id);

                if (category == null)
                {
                    throw new NotFoundException(nameof(Category), request.Id);
                }

                category.Name = request.Name;

                await this.context.SaveChangesAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(Category), request.Name, Constants.Common.Edited);
            }
        }
    }
}