using Dapper;
using GameBox.Scheduler.Extensions;
using GameBox.Scheduler.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Scheduler.Services
{
    public class RedisCacheHostedService : BackgroundService
    {
        private readonly string redisConnectionString;
        private readonly string databaseConnectionString;
        private readonly IDistributedCache cache;

        public RedisCacheHostedService(
            IDistributedCache cache,
            IConfiguration configuration)
        {
            this.cache = cache;
            this.redisConnectionString = configuration.GetConnectionString("Redis");
            this.databaseConnectionString = configuration.GetConnectionString("Database");
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