using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Extensions;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Query.Products;

public sealed class GetProductsQueryHandler
    : IQueryHandler<GetProductsQuery, ICollection<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ICollection<ProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        if (request.PaginateRequest is not null)
        {
            var products = _unitOfWork.Products.GetProductsWithCategories(request.Expression);

            var paginateResult = await products.PaginageListAsync(
                request.PaginateRequest,
                cancellationToken
            );

            return _mapper.Map<List<ProductResponse>>(paginateResult);
        }

        return _mapper.Map<ICollection<ProductResponse>>(
            await _unitOfWork.Products.GetProductsWithCategories(request.Expression).ToListAsync()
        );
    }
}
