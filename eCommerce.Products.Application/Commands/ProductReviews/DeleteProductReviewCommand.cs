using eCommerce.Products.Application.Abstractions.Commands;

namespace eCommerce.Products.Application.Commands.ProductReviews;

public sealed record DeleteProductReviewCommand(int ProductId, int ReviewId) : ICommand;
