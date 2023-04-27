using eCommerce.Products.Application.Abstractions.Commands;
using eCommerce.Products.Application.Responses.Products;

namespace eCommerce.Products.Application.Commands.Products;

public sealed record CreateProductCommand(
    string Name,
    string Code,
    string Description,
    string Characteristics,
    string UnitOfMeassure,
    double Price = default,
    bool IsAvailable = true,
    double Rating = default
) : ICommand<CreateProductResponse>;
