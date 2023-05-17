namespace GameBox.Application.GraphQL.Wishlists;

public class RemoveGameInputType : InputObjectType<RemoveGameInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<RemoveGameInput> descriptor)
    {
        descriptor.Description("Represents the input to remove a game.");

        descriptor
            .Field(g => g.GameId)
            .Description("Represents the game id for the game.");

        base.Configure(descriptor);
    }
}
