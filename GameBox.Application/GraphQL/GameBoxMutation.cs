using GameBox.Application.Contracts.Services;
using GraphQL;
using GraphQL.Types;
using System;
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "gameId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        //ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        //return Enumerable.Empty<GameType>();
                    }

                    var gameId = ctx.GetArgument<Guid>("gameId");
                    var username = ctx.GetArgument<string>("username");

                    // user.Identity.Name;

                    try
                    {
                        return await wishlistService.AddGameToWishlistAsync(username, gameId);
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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "gameId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username" }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        //ctx.Errors.Add(new ExecutionError("Not Authenticated"));
                        //return Enumerable.Empty<GameType>();
                    }

                    var gameId = ctx.GetArgument<Guid>("gameId");
                    var username = ctx.GetArgument<string>("username");

                    // user.Identity.Name;

                    try
                    {
                        return await wishlistService.RemoveGameFromWishlistAsync(username, gameId);
                    }
                    catch (Exception e)
                    {
                        var message = e.InnerException?.Message ?? e.Message;
                        ctx.Errors.Add(new ExecutionError(message));

                        return default;
                    }
                });
        }
    }
}
