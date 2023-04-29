using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Command.Products;

public sealed class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ProductResponse> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product =
            await _unitOfWork.Products.GetByCondition(p => p.Id == request.Id).FirstOrDefaultAsync()
            ?? throw new ItemNotFoundException(typeof(Product), request.Id);

        _mapper.Map(request, product);

        if (request.Categories is not null && request.Categories.Any())
        {
            var categories = await _unitOfWork.Categories
                .GetByCondition(c => request.Categories.Contains(c.Name))
                .ToListAsync();

            product.Categories = categories;
        }

        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductResponse>(product);
    }
}
