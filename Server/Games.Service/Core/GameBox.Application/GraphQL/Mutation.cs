using GameBox.Application.Contracts.Services;
using GameBox.Application.Exceptions;
using GameBox.Application.GraphQL.Wishlists;
using GameBox.Domain.Entities;

namespace GameBox.Application.GraphQL;

[GraphQLDescription("Represents the mutations available.")]
public class Mutation
{
    private readonly IDataService context;
    private readonly ICurrentUserService currentUser;

    public Mutation(
        IDataService context,
        ICurrentUserService currentUser)
    {
        this.context = context;
        this.currentUser = currentUser;
    }

    [GraphQLDescription("Adds a game to user's wishlist.")]
    public async Task<Guid> AddGameAsync(AddGameInput input)
    {
        var gameExists = context
            .All<Wishlist>()
            .Any(w => w.CustomerId == currentUser.CustomerId && w.GameId == input.GameId);

        if (gameExists)
        {
            return input.GameId;
        }

        var wishlist = new Wishlist
        {
            GameId = input.GameId,
            CustomerId = currentUser.CustomerId
        };

        await context.AddAsync(wishlist);
        await context.SaveAsync();

        return input.GameId;
    }

    [GraphQLDescription("Removes a game from user's wishlist.")]
    public async Task<Guid> RemoveGameAsync(RemoveGameInput input)
    {
        var wishlist = context
            .All<Wishlist>()
            .Where(w => w.CustomerId == currentUser.CustomerId)
            .Where(w => w.GameId == input.GameId)
            .FirstOrDefault();

        if (wishlist == null)
        {
            throw new NotFoundException(nameof(Game), input.GameId);
        }

        await context.DeleteAsync(wishlist);
        await context.SaveAsync();

        return input.GameId;
    }

    [GraphQLDescription("Clears all games from user's wishlist.")]
    public async Task<IEnumerable<Guid>> ClearGamesAsync()
    {
        var result = new List<Guid>();

        var games = context
            .All<Wishlist>()
            .Where(w => w.CustomerId == currentUser.CustomerId)
            .ToList();

        foreach (var game in games)
        {
            await context.DeleteAsync(game);
            result.Add(game.GameId);
        }

        await context.SaveAsync();

        return result;
    }
}
