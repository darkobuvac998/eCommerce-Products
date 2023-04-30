using eCommerce.Products.Application.Commands.ProductReviews;
using FluentValidation;

namespace eCommerce.Products.Application.Validators.ProductReviews;

public sealed class UpdateProductReviewCommandValidator
    : AbstractValidator<UpdateProductReviewCommand>
{
    public UpdateProductReviewCommandValidator()
    {
        RuleFor(x => x.ProductId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.ReviewId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.Review).NotEmpty().NotNull();
    }
}
