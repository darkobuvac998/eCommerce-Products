using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Application.Responses.Products;

public sealed class ProductReviewResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }

    public int UserId { get; set; }
    public string Username { get; set; }

    [MaxLength(500)]
    public string Review { get; set; }
}
