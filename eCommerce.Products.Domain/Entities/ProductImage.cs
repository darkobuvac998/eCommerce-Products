using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Domain.Entities;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    [MaxLength(255)]
    public Uri ImageUrl { get; set; }
    public virtual Product Product { get; set; }
}
