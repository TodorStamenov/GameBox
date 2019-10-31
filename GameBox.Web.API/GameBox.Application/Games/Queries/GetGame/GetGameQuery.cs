using MediatR;
using System;

namespace GameBox.Application.Games.Queries.GetGame
{
    public class GetGameQuery : IRequest<GameViewModel>
    {
        public Guid Id { get; set; }
    }
}
