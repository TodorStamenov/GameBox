using MediatR;
using System;

namespace GameBox.Application.Games.Commands.UpdateGame
{
    public class UpdateGameCommand : IRequest<string>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Size { get; set; }

        public string VideoId { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Description { get; set; }

        public string ReleaseDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}
