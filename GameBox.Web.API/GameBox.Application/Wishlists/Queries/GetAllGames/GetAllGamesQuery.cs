using GameBox.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace GameBox.Application.Wishlists.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>>
    {
        public Guid UserId { get; set; }
    }
}
