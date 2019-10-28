using GameBox.Application.Contracts.Services;
using GameBox.Application.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using System.Linq;
using System.Security.Claims;

namespace GameBox.Application.GraphQL
{
    public class GameBoxQuery : ObjectGraphType
    {
        public GameBoxQuery(IWishlistService wishlistService)
        {
            FieldAsync<ListGraphType<GameType>>(
                "wishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        //ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        //return Enumerable.Empty<GameType>();
                    }

                    var username = ctx.GetArgument<string>("username");

                    // user.Identity.Name;

                    return await wishlistService.GetWishlistGamesAsync(username);
                });
        }
    }
}
