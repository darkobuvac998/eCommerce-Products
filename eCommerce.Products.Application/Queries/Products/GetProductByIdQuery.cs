using eCommerce.Products.Application.Abstractions.Queries;
using eCommerce.Products.Application.Responses.Products;

namespace eCommerce.Products.Application.Queries.Products;

public sealed record GetProductByIdQuery(int Id) : IQuery<ProductResponse>;
