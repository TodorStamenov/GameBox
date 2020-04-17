using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsAdmin { get; set; }

        public string Content { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, string>
        {
            private readonly IGameBoxDbContext context;

            public UpdateCommentCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await this.context.Comments.FindAsync(request.Id);

                if (comment == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                if (comment.UserId != request.UserId && !request.IsAdmin)
                {
                    throw new NoAccessException();
                }

                comment.Content = request.Content;

                await this.context.SaveChangesAsync();

                return string.Format(Constants.Common.Success, nameof(Comment), string.Empty, Constants.Common.Edited);
            }
        }
    }
}