using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Command.ProductReviews;

public sealed class DeleteProductReviewCommandHandler : ICommandHandler<DeleteProductReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductReviewCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(
        DeleteProductReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var review =
            (
                await _unitOfWork.ProductReviews
                    .GetByCondition(
                        pr => pr.Id == request.ReviewId && pr.ProductId == request.ProductId
                    )
                    .FirstOrDefaultAsync()
            ) ?? throw new ItemNotFoundException(typeof(ProductReview), request.ReviewId);

        await _unitOfWork.ProductReviews.RemoveAsync(review);
        await _unitOfWork.SaveChangesAsync();
    }
}
