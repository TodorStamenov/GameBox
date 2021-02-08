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

        public GameBoxMutation(IMediator mediator)
        {
            FieldAsync<IdGraphType>(
                "addGameToWishlist",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = GameId }),
                resolve: async ctx =>
                {
                    try
                    {
                        var gameId = ctx.GetArgument<Guid>(GameId);

                        return await mediator.Send(new AddGameCommand { GameId = gameId });
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
                    try
                    {
                        var gameId = ctx.GetArgument<Guid>(GameId);

                        return await mediator.Send(new RemoveGameCommand { GameId = gameId });
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
                    return await mediator.Send(new ClearGamesCommand());
                });
        }
    }
}
