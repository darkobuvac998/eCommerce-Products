using eCommerce.Products.Application.Abstractions.Queries;
using eCommerce.Products.Application.Responses.ProductReviews;

namespace eCommerce.Products.Application.Queries.ProductReviews;

public sealed record GetProductReviewsQuery(int ProductId)
    : IQuery<ICollection<ProductReviewResponse>>;
