namespace eCommerce.Products.Presentation.DTOs.ProductReview;

public sealed class UpdateProductReview
{
    public int ProductId { get; set; }
    public int ReviewId { get; set; }
    public string Review { get; set; }
}
