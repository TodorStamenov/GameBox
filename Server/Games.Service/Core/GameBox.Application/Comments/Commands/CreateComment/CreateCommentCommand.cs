using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<string>
{
    public Guid GameId { get; set; }

    public string Content { get; set; }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, string>
    {
        private readonly IDataService context;
        private readonly IDateTimeService dateTime;
        private readonly ICurrentUserService currentUser;

        public CreateCommentCommandHandler(
            IDataService context,
            IDateTimeService dateTime,
            ICurrentUserService currentUser)
        {
            this.context = context;
            this.dateTime = dateTime;
            this.currentUser = currentUser;
        }

        public async Task<string> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                GameId = request.GameId,
                CustomerId = this.currentUser.CustomerId,
                Content = request.Content,
                DateAdded = this.dateTime.UtcNow
            };

            await this.context.AddAsync(comment);
            await this.context.SaveAsync();

            return string.Format(Constants.Common.Success, nameof(Comment), string.Empty, Constants.Common.Added);
        }
    }
}
