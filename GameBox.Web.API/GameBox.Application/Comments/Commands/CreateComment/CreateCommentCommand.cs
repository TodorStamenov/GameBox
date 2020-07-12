using GameBox.Application.Contracts.Services;
using GameBox.Application.Infrastructure;
using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<string>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public string Content { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, string>
        {
            private readonly IDataService context;
            private readonly IDateTimeService dateTime;

            public CreateCommentCommandHandler(IDataService context, IDateTimeService dateTime)
            {
                this.context = context;
                this.dateTime = dateTime;
            }

            public async Task<string> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = new Comment
                {
                    GameId = request.GameId,
                    UserId = request.UserId,
                    Content = request.Content,
                    TimeStamp = this.dateTime.UtcNow
                };

                await this.context.AddAsync(comment);
                await this.context.SaveAsync();

                return string.Format(Constants.Common.Success, nameof(Comment), string.Empty, Constants.Common.Added);
            }
        }
    }
}