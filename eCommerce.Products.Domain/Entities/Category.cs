using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Domain.Entities;

public class Category : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
