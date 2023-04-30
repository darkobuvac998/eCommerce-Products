using eCommerce.Products.Application.Commands.ProductReviews;
using FluentValidation;

namespace eCommerce.Products.Application.Validators.ProductReviews;

public sealed class DeleteProductReviewCommandValidator
    : AbstractValidator<DeleteProductReviewCommand>
{
    public DeleteProductReviewCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(x => x.ReviewId).NotEmpty().NotNull().GreaterThan(0);
    }
}
