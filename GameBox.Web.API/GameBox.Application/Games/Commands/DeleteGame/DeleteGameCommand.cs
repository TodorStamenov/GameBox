using MediatR;
using System;

namespace GameBox.Application.Games.Commands.DeleteGame
{
    public class DeleteGameCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
