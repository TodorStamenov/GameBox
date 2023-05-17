using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GameBox.Application.Infrastructure.Extensions;

public static class DistributedCacheExtensions
{
    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
    {
        var jsonData = await cache.GetStringAsync(recordId);

        if (jsonData is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }
}
