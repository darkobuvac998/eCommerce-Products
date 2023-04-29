using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Command.Products;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product =
            await _unitOfWork.Products.GetByCondition(p => p.Id == request.Id).FirstOrDefaultAsync()
            ?? throw new ItemNotFoundException(typeof(Product), request.Id);

        await _unitOfWork.Products.RemoveAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
