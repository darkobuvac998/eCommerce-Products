using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;

namespace eCommerce.Products.Application.Handlers.Command.Products;

public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<CreateProductResponse> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = _mapper.Map<Product>(request);

        await _unitOfWork.Products.AddAsync(product, cancellationToken);

        return _mapper.Map<CreateProductResponse>(product);
    }
}
