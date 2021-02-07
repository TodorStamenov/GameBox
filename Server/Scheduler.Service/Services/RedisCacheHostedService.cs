using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Scheduler.Service.Extensions;
using Scheduler.Service.Model;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Service.Services
{
    public class RedisCacheHostedService : BackgroundService
    {
        private readonly string databaseConnectionString;
        private readonly IDistributedCache cache;

        public RedisCacheHostedService(
            IDistributedCache cache,
            IConfiguration configuration)
        {
            this.cache = cache;
            this.databaseConnectionString = configuration.GetConnectionString("Games");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.UpdateDataAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task UpdateDataAsync()
        {
            using (var connection = new SqlConnection(this.databaseConnectionString))
            {
                var games = await connection.QueryAsync<GamesCacheModel>(
                    @"SELECT Id, CategoryId, Title, Description, VideoId, ThumbnailUrl, Price, Size, ViewCount
                        FROM Games
                    ORDER BY ReleaseDate DESC, ViewCount DESC, Title ASC");

                await this.cache.SetRecordAsync("_Games_", games);
            }
        }
    }
}