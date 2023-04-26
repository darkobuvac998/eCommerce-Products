using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Code { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    public string ProductCharacteristics { get; set; }
    public string UnitOfMeasure { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public double Rating { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<ProductImage> Images { get; set; }
    public virtual ICollection<ProductReview> Reviews { get; set; }
}
