using GameBox.Application.Contracts.Services;
using GameBox.Domain.Entities;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;

namespace GameBox.Application.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {
        private readonly IDataService context;
        private readonly ICurrentUserService currentUser;

        public Query(
            IDataService context,
            ICurrentUserService currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets all games in user's wishlist.")]
        public IQueryable<Game> GetWishlist()
        {
            return this.context
                .All<Wishlist>()
                .Where(w => w.CustomerId == currentUser.CustomerId)
                .Select(w => w.Game)
                .OrderBy(g => g.Title);
        }
    }
}
