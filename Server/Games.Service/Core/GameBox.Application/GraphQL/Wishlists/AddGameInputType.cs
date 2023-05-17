namespace GameBox.Application.GraphQL.Wishlists;

public class AddGameInputType : InputObjectType<AddGameInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<AddGameInput> descriptor)
    {
        descriptor.Description("Represents the input to add for a game.");

        descriptor
            .Field(g => g.GameId)
            .Description("Represents the game id for the game.");

        base.Configure(descriptor);
    }
}
