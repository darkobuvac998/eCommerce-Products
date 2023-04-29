using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Command.Products;

public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ProductResponse> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = _mapper.Map<Product>(request);

        if (request.Categories.Any())
        {
            var categories = _unitOfWork.Categories.GetByCondition(
                c => request.Categories.Contains(c.Name)
            );

            product.Categories = await categories.ToListAsync();
        }

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductResponse>(product);
    }
}
