using eCommerce.Products.Domain.Contracts.Repositories;

namespace eCommerce.Products.Domain.Contracts;

public interface IUnitOfWork
{
    IProductRepository Products { get; }

    Task SaveChangesAsync();
    Task RollbackChangesAsync();
    Task DisposeAsync();
}
