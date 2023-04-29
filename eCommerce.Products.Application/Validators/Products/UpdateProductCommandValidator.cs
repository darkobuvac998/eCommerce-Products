using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Validators.Products;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).NotNull().NotEmpty();
        //RuleFor(x => x.Id)
        //    .MustAsync(
        //        async (id, cancellationToken) =>
        //        {
        //            var product = await _unitOfWork.Products
        //                .GetByCondition(p => p.Id == id)
        //                .FirstOrDefaultAsync();

        //            return product is not null;
        //        }
        //    )
        //    .WithMessage(
        //        $"Entity of type {typeof(Product)} with identifier {{PropertyValue}} not found"
        //    );
    }
}
