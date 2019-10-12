using MediatR;
using System;

namespace GameBox.Application.Games.Queries.GetGameDetails
{
    public class GetGameDetailsQuery : IRequest<GameDetailsViewModel>
    {
        public Guid Id { get; set; }
    }
}
