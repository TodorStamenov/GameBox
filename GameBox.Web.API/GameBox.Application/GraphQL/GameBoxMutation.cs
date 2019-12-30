using GameBox.Application.Wishlists.Commands.AddGame;
using GameBox.Application.Wishlists.Commands.ClearGames;
using GameBox.Application.Wishlists.Commands.RemoveGame;
using GraphQL;
using GraphQL.Types;
using MediatR;
using System;

namespace GameBox.Application.GraphQL
{
    public class GameBoxMutation : ObjectGraphType
    {
        private const string GameId = "gameId";
        private const string UserId = "userId";

        public GameBoxMutation(IMediator mediator)
        {
            FieldAsync<IdGraphType>(
                "addGameToWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = GameId },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = UserId }),
                resolve: async ctx =>
                {
                    try
                    {
                        var gameId = ctx.GetArgument<Guid>(GameId);
                        var userId = ctx.GetArgument<Guid>(UserId);

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
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = GameId },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = UserId }),
                resolve: async ctx =>
                {
                    try
                    {
                        var gameId = ctx.GetArgument<Guid>(GameId);
                        var userId = ctx.GetArgument<Guid>(UserId);

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
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = UserId }),
                resolve: async ctx =>
                {
                    var userId = ctx.GetArgument<Guid>(UserId);

                    return await mediator.Send(new ClearGamesCommand { UserId = userId });
                });
        }
    }
}
