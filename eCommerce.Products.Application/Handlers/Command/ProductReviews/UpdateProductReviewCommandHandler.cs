using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Application.Responses.ProductReviews;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Command.ProductReviews;

public sealed class UpdateProductReviewCommandHandler
    : ICommandHandler<UpdateProductReviewCommand, ProductReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ProductReviewResponse> Handle(
        UpdateProductReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var review =
            (
                await _unitOfWork.ProductReviews
                    .GetByCondition(
                        r => r.Id == request.ReviewId && r.ProductId == request.ProductId
                    )
                    .FirstOrDefaultAsync(cancellationToken)
            ) ?? throw new ItemNotFoundException(typeof(ProductReview), request.ReviewId);

        _mapper.Map(request, review);

        await _unitOfWork.ProductReviews.UpdateAsync(review);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductReviewResponse>(review);
    }
}
