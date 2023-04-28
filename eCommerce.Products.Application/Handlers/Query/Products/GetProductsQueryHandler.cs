using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Extensions;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Contracts;

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
            var products = await _unitOfWork.Products.GetByConditionAsync(
                request.Expression,
                cancellationToken
            );

            return _mapper.Map<ICollection<GetProductResponse>>(
                products.AsQueryable().PaginageListAsync(request.PaginateRequest, cancellationToken)
            );
        }

        return _mapper.Map<ICollection<GetProductResponse>>(
            await _unitOfWork.Products.GetByConditionAsync(request.Expression, cancellationToken)
        );
    }
}
