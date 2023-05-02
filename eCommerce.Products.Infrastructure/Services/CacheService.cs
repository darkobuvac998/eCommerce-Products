using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Domain.Shared;
using eCommerce.Products.Infrastructure.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace eCommerce.Products.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<CacheService> _logger;
    private readonly RedisOptions? _redisOptions;
    private readonly DistributedCacheEntryOptions? _distributedCacheEntryOptions;
    private readonly JsonSerializerSettings? _jsonSerializerSettings;

    public CacheService(
        IDistributedCache distributedCache,
        IOptions<RedisOptions> options,
        ILogger<CacheService> logger
    )
    {
        _distributedCache = distributedCache;
        _logger = logger;
        if (options != null)
        {
            _redisOptions = options.Value;
        }
        _distributedCacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_redisOptions!.CacheExpiration)
        };
        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedResult = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedResult))
        {
            return default;
        }

        _logger.LogInformation(
            "Fetching object of type {@Type} from cache by key {@Key} at {@DateTimeUtc}",
            typeof(T),
            key,
            DateTime.UtcNow
        );

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
            JsonConvert.SerializeObject(value, _jsonSerializerSettings),
            _distributedCacheEntryOptions!,
            cancellationToken
        );
    }
}
