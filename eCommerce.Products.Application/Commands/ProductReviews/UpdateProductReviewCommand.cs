using eCommerce.Products.Application.Abstractions.Commands;
using eCommerce.Products.Application.Responses.ProductReviews;

namespace eCommerce.Products.Application.Commands.ProductReviews;

public sealed record UpdateProductReviewCommand(int ReviewId, int ProductId, string Review)
    : ICommand<ProductReviewResponse>;
