using System.Security.Cryptography;
using System.Text;

namespace eCommerce.Products.Application.Extensions;

public static class StringExtensions
{
    public static string GetMD5Hash(this string value)
    {
        using MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(value);

        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}
