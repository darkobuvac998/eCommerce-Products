using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Products.Infrastructure.Auth;

public class PolicyRequirement : IAuthorizationRequirement
{
    public string Policy { get; }

    public PolicyRequirement(string policy)
    {
        Policy = policy;
    }
}
