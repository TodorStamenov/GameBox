using MediatR;
using System;

namespace GameBox.Application.Wishlists.Commands.AddGame
{
    public class AddGameCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }
    }
}
