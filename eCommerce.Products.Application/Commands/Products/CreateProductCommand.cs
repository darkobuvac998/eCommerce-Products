using eCommerce.Products.Application.Abstractions.Commands;
using eCommerce.Products.Application.Responses.Products;
using Newtonsoft.Json.Linq;

namespace eCommerce.Products.Application.Commands.Products;

public sealed record CreateProductCommand(
    string Name,
    string Code,
    string Description,
    JObject Characteristics,
    string UnitOfMeassure,
    IList<string> Categories,
    double Price = default,
    bool IsAvailable = true
) : ICommand<ProductResponse>;
