using GameBox.Application.Accounts.Queries.GetUserId;
using GameBox.Application.Infrastructure;
using GameBox.Application.Wishlists.Commands.AddGame;
using GameBox.Application.Wishlists.Commands.ClearGames;
using GameBox.Application.Wishlists.Commands.RemoveGame;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;
using System.Linq;
using System.Security.Claims;

namespace GameBox.Application.GraphQL
{
    public class GameBoxMutation : ObjectGraphType
    {
        private const string GameId = "gameId";

        public GameBoxMutation(IMediator mediator)
        {
            FieldAsync<IdGraphType>(
                "addGameToWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = GameId }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError(Constants.Common.Unauthorised));
                        return default(Guid);
                    }

                    var gameId = ctx.GetArgument<Guid>(GameId);

                    try
                    {
                        var userId = await mediator.Send(new GetUserIdQuery { Username = user.Identity.Name });

                        return await mediator.Send(new AddGameCommand { UserId = userId, GameId = gameId });
                    }
                    catch (Exception e)
                    {
                        var message = e.InnerException?.Message ?? e.Message;
                        ctx.Errors.Add(new ExecutionError(message));

                        return default(Guid);
                    }
                });

            FieldAsync<IdGraphType>(
                "removeGameFromWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = GameId }),
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError(Constants.Common.Unauthorised));
                        return default(Guid);
                    }

                    var gameId = ctx.GetArgument<Guid>(GameId);

                    try
                    {
                        var userId = await mediator.Send(new GetUserIdQuery { Username = user.Identity.Name });

                        return await mediator.Send(new RemoveGameCommand { UserId = userId, GameId = gameId });
                    }
                    catch (Exception e)
                    {
                        var message = e.InnerException?.Message ?? e.Message;
                        ctx.Errors.Add(new ExecutionError(message));

                        return default(Guid);
                    }
                });

            FieldAsync<ListGraphType<IdGraphType>>(
                "clearGamesFromWishlist",
                resolve: async ctx =>
                {
                    var user = (ClaimsPrincipal)ctx.UserContext;

                    if (!user.Identity.IsAuthenticated)
                    {
                        ctx.Errors.Add(new ExecutionError(Constants.Common.Unauthorised));
                        return Enumerable.Empty<Guid>();
                    }

                    var userId = await mediator.Send(new GetUserIdQuery { Username = user.Identity.Name });

                    return await mediator.Send(new ClearGamesCommand { UserId = userId });
                });
        }
    }
}
