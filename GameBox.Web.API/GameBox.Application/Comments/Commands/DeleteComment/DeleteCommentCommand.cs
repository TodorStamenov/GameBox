using GameBox.Application.Contracts;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace GameBox.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, string>
        {
            private readonly IGameBoxDbContext context;

            public DeleteCommentCommandHandler(IGameBoxDbContext context)
            {
                this.context = context;
            }

            public async Task<string> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await this.context.Set<Comment>().FindAsync(request.Id);

                if (comment == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                this.context.Set<Comment>().Remove(comment);
                await this.context.SaveChangesAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(Comment), string.Empty, Constants.Common.Deleted);
            }
        }
    }
}