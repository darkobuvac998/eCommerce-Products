using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Application.Responses.ProductReviews;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;

namespace eCommerce.Products.Application.Handlers.Command.ProductReviews;

public sealed class CreateProductReviewCommandHandler
    : ICommandHandler<CreateProductReviewCommand, ProductReviewResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
        (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<ProductReviewResponse> Handle(
        CreateProductReviewCommand request,
        CancellationToken cancellationToken
    )
    {
        var review = _mapper.Map<ProductReview>(request);

        await _unitOfWork.ProductReviews.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductReviewResponse>(review);
    }
}
