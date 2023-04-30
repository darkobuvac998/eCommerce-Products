using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Domain.Shared;
using eCommerce.Products.Infrastructure.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace eCommerce.Products.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly RedisOptions? _redisOptions;
    private readonly DistributedCacheEntryOptions? _distributedCacheEntryOptions;

    public CacheService(IDistributedCache distributedCache, IOptions<RedisOptions> options)
    {
        _distributedCache = distributedCache;
        if (options != null)
        {
            _redisOptions = options.Value;
        }
        _distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_redisOptions!.CacheExpiration)
        };
    }

    public async Task<T?> GetAllAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedResult = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedResult))
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(cachedResult);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        CancellationToken cancellationToken = default
    )
    {
        await _distributedCache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(
                value,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            ),
            _distributedCacheEntryOptions!,
            cancellationToken
        );
    }
}
