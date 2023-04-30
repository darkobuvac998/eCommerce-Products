using eCommerce.Products.Application.Extensions;

namespace eCommerce.Products.Application.Shared;

public static class Utils
{
    public static string BuildCacheKey(params string?[] values)
    {
        var joinedString = string.Join("", values);
        return joinedString.GetMD5Hash();
    }
}
