using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace eCommerce.Products.Infrastructure.Auth;

public class PolicyAuthorizationHandler : AuthorizationHandler<PolicyRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PolicyRequirement requirement
    )
    {
        string? userId = context.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;

        if (!int.TryParse(userId, out var id))
        {
            return Task.CompletedTask;
        }

        // Used when user permissiona are added to jwt as a
        // permissions claim
        HashSet<string> permissions = context.User.Claims
            .Where(x => x.Type == "permissions")
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Policy))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
