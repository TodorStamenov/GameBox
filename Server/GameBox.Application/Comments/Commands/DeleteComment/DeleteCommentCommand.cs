using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace GameBox.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, string>
        {
            private readonly IDataService context;

            public DeleteCommentCommandHandler(IDataService context)
            {
                this.context = context;
            }

            public async Task<string> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = this.context
                    .All<Comment>()
                    .Where(c => c.Id == request.Id)
                    .FirstOrDefault();

                if (comment == null)
                {
                    throw new NotFoundException(nameof(Comment), request.Id);
                }

                await this.context.DeleteAsync(comment);
                await this.context.SaveAsync(cancellationToken);

                return string.Format(Constants.Common.Success, nameof(Comment), string.Empty, Constants.Common.Deleted);
            }
        }
    }
}