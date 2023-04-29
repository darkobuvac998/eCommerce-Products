using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Extensions;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;

namespace eCommerce.Products.Application.Handlers.Query.Products;

public sealed class GetProductsQueryHandler
    : IQueryHandler<GetProductsQuery, ICollection<GetProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ICollection<GetProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        if (request.PaginateRequest is not null)
        {
            var products = await _unitOfWork.Products.GetProductsWithCategoryAsync(
                request.Expression,
                cancellationToken
            );

            var paginateResult = await products.PaginageListAsync(
                request.PaginateRequest,
                cancellationToken
            );

            return _mapper.Map<List<GetProductResponse>>(paginateResult);
        }

        return _mapper.Map<ICollection<GetProductResponse>>(
            await _unitOfWork.Products.GetByConditionAsync(request.Expression, cancellationToken)
        );
    }
}
