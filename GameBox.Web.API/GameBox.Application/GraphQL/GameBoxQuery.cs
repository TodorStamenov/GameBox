using GameBox.Application.GraphQL.Types;
using GameBox.Application.Wishlists.Queries.GetAllGames;
using GraphQL.Types;
using MediatR;
using System;

namespace GameBox.Application.GraphQL
{
    public class GameBoxQuery : ObjectGraphType
    {
        private const string UserId = "userId";

        public GameBoxQuery(IMediator mediator)
        {
            FieldAsync<ListGraphType<GameType>>(
                "wishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = UserId }),
                resolve: async ctx =>
                {
                    var userId = ctx.GetArgument<Guid>(UserId);

                    return await mediator.Send(new GetAllGamesQuery { UserId = userId });
                });
        }
    }
}
