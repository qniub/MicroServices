using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace MicroServices.Web
{
    public static class DistributedCacheExtensions
    {
        public static void SetObject(
            this IDistributedCache cache, string key, object value)
        {
            string text = JsonSerializer.Serialize(value);

            cache.SetString(key, text);
        }

        public static void SetObject(
            this IDistributedCache cache,
            string key,
            object value,
            TimeSpan absoluteExpiration)
        {
            string text = JsonSerializer.Serialize(value);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration,
            };

            cache.SetString(key, text, options);
        }

        public static void SetObject(
            this IDistributedCache cache,
            string key,
            object value,
            DistributedCacheEntryOptions options)
        {
            string text = JsonSerializer.Serialize(value);

            cache.SetString(key, text, options);
        }

        public static bool TryGetObject<T>(
            this IDistributedCache cache, string key, out T value)
        {
            string text = cache.GetString(key);

            value = text != null
                ? JsonSerializer.Deserialize<T>(text)
                : default;

            return value != null;
        }
    }
}
