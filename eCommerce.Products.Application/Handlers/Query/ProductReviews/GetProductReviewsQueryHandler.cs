using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Queries.ProductReviews;
using eCommerce.Products.Application.Responses.ProductReviews;
using eCommerce.Products.Application.Shared;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Query.ProductReviews;

public class GetProductReviewsQueryHandler
    : IQueryHandler<GetProductReviewsQuery, ICollection<ProductReviewResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductReviewsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService
    ) => (_unitOfWork, _mapper, _cacheService) = (unitOfWork, mapper, cacheService);

    public async Task<ICollection<ProductReviewResponse>> Handle(
        GetProductReviewsQuery request,
        CancellationToken cancellationToken
    )
    {
        var key = Utils.BuildCacheKey("product-review", $"{request.ProductId}");

        var cachedReviews = await _cacheService.GetAsync<IList<ProductReview>>(
            key,
            cancellationToken
        );

        if (cachedReviews != null && cachedReviews.Any())
            return _mapper.Map<List<ProductReviewResponse>>(cachedReviews);

        var reviews = await _unitOfWork.ProductReviews
            .GetByCondition(p => p.ProductId == request.ProductId)
            .AsNoTracking()
            .ToListAsync();

        await _cacheService.SetAsync(key, reviews);

        return _mapper.Map<List<ProductReviewResponse>>(reviews);
    }
}
