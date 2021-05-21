using Grpc.Net.Client;
using System.Threading.Tasks;

namespace Seeder.Service
{
    public class Program
    {
        private const string BaseApiUrl = "http://172.17.0.1:";

        public static async Task Main()
        {
            var seeded = await SeedUsersDatabaseAsync();

            if (seeded)
            {
                await Task.Delay(5000);
                await SeedGamesDatabaseAsync();
            }
        }

        private static async Task<bool> SeedUsersDatabaseAsync()
        {
            using (var channel = GrpcChannel.ForAddress($"{BaseApiUrl}5000"))
            {
                var client = new UsersSeeder.UsersSeederClient(channel);
                var response = await client.SeedUsersDatabaseAsync(new SeedUsersRequest());
                return response.Seeded;
            }
        }

        private static async Task<bool> SeedGamesDatabaseAsync()
        {
            using (var channel = GrpcChannel.ForAddress($"{BaseApiUrl}5001"))
            {
                var client = new GamesSeeder.GamesSeederClient(channel);
                var response = await client.SeedGamesDatabaseAsync(new SeedGamesRequest());
                return response.Seeded;
            }
        }
    }
}
