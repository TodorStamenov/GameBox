using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Wishlists.Commands.ClearGames
{
    public class ClearGamesCommand : IRequest<IEnumerable<Guid>>
    {
        public Guid UserId { get; set; }
    }
}
