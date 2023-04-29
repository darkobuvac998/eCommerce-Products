using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Queries.ProductReviews;
using eCommerce.Products.Application.Responses.ProductReviews;
using eCommerce.Products.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Query.ProductReviews;

public class GetProductReviewsQueryHandler
    : IQueryHandler<GetProductReviewsQuery, ICollection<ProductReviewResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ICollection<ProductReviewResponse>> Handle(
        GetProductReviewsQuery request,
        CancellationToken cancellationToken
    )
    {
        var reviews = await _unitOfWork.ProductReviews
            .GetByCondition(p => p.ProductId == request.ProductId)
            .ToListAsync();

        return _mapper.Map<List<ProductReviewResponse>>(reviews);
    }
}
