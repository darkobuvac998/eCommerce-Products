using System.ComponentModel.DataAnnotations;

namespace eCommerce.Products.Application.Responses.ProductReviews;

public sealed class ProductReviewResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }

    [MaxLength(500)]
    public string Review { get; set; }
}
