using MediatR;
using System;

namespace GameBox.Application.Wishlists.Commands.RemoveGame
{
    public class RemoveGameCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }
    }
}
