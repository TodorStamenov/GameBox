using GameBox.Application.GraphQL.Types;
using GameBox.Application.Wishlists.Queries.GetAllGames;
using GraphQL.Types;
using MediatR;

namespace GameBox.Application.GraphQL
{
    public class GameBoxQuery : ObjectGraphType
    {
        public GameBoxQuery(IMediator mediator)
        {
            FieldAsync<ListGraphType<GameType>>(
                "wishlist",
                resolve: async ctx =>
                {
                    return await mediator.Send(new GetAllGamesQuery());
                });
        }
    }
}
