using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace eCommerce.Products.Infrastructure.Auth;

public class PolicyAuthorizationProvider : DefaultAuthorizationPolicyProvider
{
    public PolicyAuthorizationProvider(IOptions<AuthorizationOptions> options)
        : base(options) { }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PolicyRequirement(policyName))
            .Build();
    }
}
