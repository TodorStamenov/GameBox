using GameBox.Application.Accounts.Queries.GetUserId;
using GameBox.Application.GraphQL.Types;
using GameBox.Application.Infrastructure;
using GameBox.Application.Wishlists.Queries.GetAllGames;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System.Linq;
using System.Security.Claims;

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
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError(Constants.Common.Unauthorised));
                        return Enumerable.Empty<GameType>();
                    }

                    var userId = await mediator.Send(new GetUserIdQuery { Username = user.Identity.Name });

                    return await mediator.Send(new GetAllGamesQuery { UserId = userId });
                });
        }
    }
}
