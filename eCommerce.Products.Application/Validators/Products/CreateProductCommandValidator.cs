using eCommerce.Products.Application.Commands.Products;
using FluentValidation;

namespace eCommerce.Products.Application.Validators.Products;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(500);
    }
}
