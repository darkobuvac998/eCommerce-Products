using eCommerce.Products.Application.Abstractions.Commands;
using eCommerce.Products.Application.Responses.ProductReviews;

namespace eCommerce.Products.Application.Commands.ProductReviews;

public sealed record CreateProductReviewCommand(
    int ProductId,
    int UserId,
    string Username,
    string Review
) : ICommand<ProductReviewResponse>;
