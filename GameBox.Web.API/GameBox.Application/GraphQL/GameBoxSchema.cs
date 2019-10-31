using GraphQL;
using GraphQL.Types;

namespace GameBox.Application.GraphQL
{
    public class GameBoxSchema : Schema
    {
        public GameBoxSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<GameBoxQuery>();
            Mutation = resolver.Resolve<GameBoxMutation>();
        }
    }
}
