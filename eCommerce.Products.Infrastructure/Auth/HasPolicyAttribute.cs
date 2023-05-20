using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Products.Infrastructure.Auth;

public sealed class HasPolicyAttribute : AuthorizeAttribute
{
    public HasPolicyAttribute(string policy)
        : base(policy: policy) { }
}
