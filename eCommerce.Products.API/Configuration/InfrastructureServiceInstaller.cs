using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Infrastructure.Auth;
using eCommerce.Products.Infrastructure.Options;
using eCommerce.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Products.API.Configuration;

public sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<RedisOptions>()
            .Bind(configuration.GetSection(nameof(RedisOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<ICacheService, CacheService>();

        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PolicyAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PolicyAuthorizationProvider>();
    }
}
