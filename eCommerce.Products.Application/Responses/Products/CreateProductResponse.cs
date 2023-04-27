namespace eCommerce.Products.Application.Responses.Products;

public sealed record CreateProductResponse(
    int Id,
    string Name,
    string Code,
    string Description,
    string Characteristics,
    string UnitOfMeassure,
    double Price,
    bool IsAvailable,
    double Rating
);
