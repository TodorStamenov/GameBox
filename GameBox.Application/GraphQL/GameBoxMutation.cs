using GameBox.Application.Contracts.Services;
using GraphQL;
using GraphQL.Types;
using System;
using System.Linq;
using System.Security.Claims;

namespace GameBox.Application.GraphQL
{
    public class GameBoxMutation : ObjectGraphType
    {
        public GameBoxMutation(IWishlistService wishlistService)
        {
            FieldAsync<IdGraphType>(
                "addGameToWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "gameId" }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        return default(Guid);
                    }

                    var gameId = ctx.GetArgument<Guid>("gameId");

                    try
                    {
                        return await wishlistService.AddGameToWishlistAsync(user.Identity.Name, gameId);
                    }
                    catch (Exception e)
                    {
                        var message = e.InnerException?.Message ?? e.Message;
                        ctx.Errors.Add(new ExecutionError(message));

                        return default;
                    }
                });

            FieldAsync<IdGraphType>(
                "removeGameFromWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "gameId" }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        return default(Guid);
                    }

                    var gameId = ctx.GetArgument<Guid>("gameId");

                    try
                    {
                        return await wishlistService.RemoveGameFromWishlistAsync(user.Identity.Name, gameId);
                    }
                    catch (Exception e)
                    {
                        var message = e.InnerException?.Message ?? e.Message;
                        ctx.Errors.Add(new ExecutionError(message));

                        return default;
                    }
                });

            FieldAsync<ListGraphType<IdGraphType>>(
                "clearGamesFromWishlist",
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        return Enumerable.Empty<Guid>();
                    }

                    return await wishlistService.ClearGamesFromWishlistAsync(user.Identity.Name);
                });
        }
    }
}
