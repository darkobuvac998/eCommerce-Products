using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Domain.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Validators.ProductReviews;

public class CreateProductReviewCommandValidator : AbstractValidator<CreateProductReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductReviewCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.ProductId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .MustAsync(
                async (id, cancellationToken) =>
                {
                    var product = await _unitOfWork.Products
                        .GetByCondition(p => p.Id == id)
                        .FirstOrDefaultAsync();

                    return product is not null;
                }
            );

        RuleFor(x => x.Review).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.UserId).NotNull().GreaterThan(0);
    }
}
