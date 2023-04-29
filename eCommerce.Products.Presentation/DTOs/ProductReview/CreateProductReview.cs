namespace eCommerce.Products.Presentation.DTOs.ProductReview;

public class CreateProductReview
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Review { get; set; }
}
