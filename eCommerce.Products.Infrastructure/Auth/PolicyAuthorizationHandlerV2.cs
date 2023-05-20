using eCommerce.Products.Domain.Contracts.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace eCommerce.Products.Infrastructure.Auth;

public class PolicyAuthorizationHandlerV2 : AuthorizationHandler<PolicyRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PolicyAuthorizationHandlerV2(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PolicyRequirement requirement
    )
    {
        string? userId = context.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (!int.TryParse(userId, out var id))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        ICacheService cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

        var cacheKey = $"user-{userId}";

        // Fetching users permissions from Redis
        var permissions = await cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (permissions is null)
        {
            return;
        }

        if (permissions.Contains(requirement.Policy))
        {
            context.Succeed(requirement);
        }
    }
}
