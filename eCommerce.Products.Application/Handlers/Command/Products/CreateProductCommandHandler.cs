using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;

namespace eCommerce.Products.Application.Handlers.Command.Products;

public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public Task<CreateProductResponse> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
