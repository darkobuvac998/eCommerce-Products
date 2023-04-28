using eCommerce.Products.Application.Abstractions.Queries;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Shared;
using System.Linq.Expressions;

namespace eCommerce.Products.Application.Queries.Products;

public sealed record GetProductsQuery(
    Expression<Func<Product, bool>>? Expression,
    PaginateRequest? PaginateRequest
) : IQuery<ICollection<GetProductResponse>>;
