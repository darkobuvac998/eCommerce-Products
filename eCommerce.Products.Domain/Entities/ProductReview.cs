using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Domain.Entities;

public class ProductReview : BaseEntity
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }

    [MaxLength(500)]
    public string Review { get; set; }

    public virtual Product Product { get; set; }
}
