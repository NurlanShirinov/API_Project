using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TurboAz.Service.Extensions
{
    public static class DistributedCacheExtensions
    {
        public async static Task<T> GetCachedValueAsyn<T>(this IDistributedCache cache, string key, CancellationToken token = default(CancellationToken)) where T : class
        {
            var result = await cache.GetAsync(key, token);
            return result.FromByteArray<T>();
        }

        public async static Task SetCachedValueAsync<T>(this IDistributedCache cache, string key, T value, CancellationToken token = default(CancellationToken))
        {
            await cache.SetAsync(key, value.ToByteArray(), token);
        }
    }
}
