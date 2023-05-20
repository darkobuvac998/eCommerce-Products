namespace eCommerce.Products.Infrastructure.Auth;

public sealed class Policy
{
    private readonly string _policyName;

    public string PolicyName
    {
        get => _policyName;
    }

    public Policy(string resource, string scope)
    {
        _policyName = $"{resource}-{scope}";
    }

    public static string Build(string resource, string scope)
    {
        return $"{resource}-{scope}";
    }
}
