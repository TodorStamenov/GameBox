using GraphQL.Types;

namespace GameBox.Application.GraphQL.Types
{
    public class GameInputType : InputObjectGraphType
    {
        public GameInputType()
        {
            Name = "wishlistInput";
            Field<NonNullGraphType<IdGraphType>>("gameId");
        }
    }
}
