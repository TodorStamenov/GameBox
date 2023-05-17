using Grpc.Net.Client;
using GrpcGamesSeederClient;
using GrpcUsersSeederClient;

namespace Seeder.Service;

public class Program
{
    private const string BaseApiUrl = "https://172.17.0.1:";

    public static async Task Main()
    {
        var channelOptions = new GrpcChannelOptions
        {
            HttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            }
        };

        var seeded = await SeedUsersDatabaseAsync(channelOptions);
        if (seeded)
        {
            await Task.Delay(5000);
            await SeedGamesDatabaseAsync(channelOptions);
        }
    }

    private static async Task<bool> SeedUsersDatabaseAsync(GrpcChannelOptions channelOptions)
    {
        using (var channel = GrpcChannel.ForAddress($"{BaseApiUrl}6001", channelOptions))
        {
            var client = new UsersSeeder.UsersSeederClient(channel);
            var response = await client.SeedUsersDatabaseAsync(new SeedUsersRequest());
            return response.Seeded;
        }
    }

    private static async Task<bool> SeedGamesDatabaseAsync(GrpcChannelOptions channelOptions)
    {
        using (var channel = GrpcChannel.ForAddress($"{BaseApiUrl}6000", channelOptions))
        {
            var client = new GamesSeeder.GamesSeederClient(channel);
            var response = await client.SeedGamesDatabaseAsync(new SeedGamesRequest());
            return response.Seeded;
        }
    }
}
