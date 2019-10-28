using GameBox.Domain.Entities;
using GraphQL.Types;

namespace GameBox.Application.GraphQL.Types
{
    public class GameType : ObjectGraphType<Game>
    {
        public GameType()
        {
            Field(g => g.Id, type: typeof(IdGraphType));
            Field(g => g.Title);
            Field(g => g.Description);
            Field(g => g.VideoId);
            Field(g => g.ThumbnailUrl, type: typeof(StringGraphType));
            Field(g => g.Price);
        }
    }
}
