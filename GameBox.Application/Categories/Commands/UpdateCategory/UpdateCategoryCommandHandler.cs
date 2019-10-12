using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
    {
        private readonly IGameBoxDbContext context;

        public UpdateCategoryCommandHandler(IGameBoxDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.context
                .Categories
                .SingleOrDefaultAsync(c => c.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;

            await this.context.SaveChangesAsync(cancellationToken);

            return string.Format(Constants.Common.Success, nameof(Category), request.Name, Constants.Common.Edited);
        }
    }
}