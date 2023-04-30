using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Domain.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Validators.Products;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .MustAsync(
                async (id, token) =>
                {
                    var product = await _unitOfWork.Products
                        .GetByCondition(x => x.Id == id)
                        .FirstOrDefaultAsync(token);

                    return product is not null;
                }
            );
    }
}
