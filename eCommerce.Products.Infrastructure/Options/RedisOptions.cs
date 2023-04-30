using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Infrastructure.Options;

public sealed class RedisOptions
{
    [Required]
    public int CacheExpiration { get; set; } = 60;
}
