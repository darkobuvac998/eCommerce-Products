using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Extensions;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Application.Shared;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Query.Products;

public sealed class GetProductsQueryHandler
    : IQueryHandler<GetProductsQuery, ICollection<ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService
    ) => (_unitOfWork, _mapper, _cacheService) = (unitOfWork, mapper, cacheService);

    public async Task<ICollection<ProductResponse>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var key = Utils.BuildCacheKey(
            "products",
            request.Expression?.ToString(),
            request.PaginateRequest?.ToString()
        );

        var cachedProducts = await _cacheService.GetAsync<IList<Product>>(key, cancellationToken);

        if (cachedProducts != null && cachedProducts.Any())
        {
            return _mapper.Map<List<ProductResponse>>(cachedProducts);
        }

        if (request.PaginateRequest is not null)
        {
            var products = _unitOfWork.Products.GetProductsDetails(request.Expression);

            var paginateResult = await products.PaginageListAsync(
                request.PaginateRequest!,
                cancellationToken
            );

            await _cacheService.SetAsync(key, paginateResult, cancellationToken);

            return _mapper.Map<List<ProductResponse>>(paginateResult);
        }

        var result = _mapper.Map<ICollection<ProductResponse>>(
            await _unitOfWork.Products
                .GetProductsDetails(request.Expression)
                .ToListAsync(cancellationToken)
        );

        await _cacheService.SetAsync(key, result, cancellationToken);

        return result;
    }
}
