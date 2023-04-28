using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductsDbContext _dbContext;

    private IProductRepository? _productRepository;

    public UnitOfWork(ProductsDbContext dbContext) => _dbContext = (dbContext);

    public IProductRepository Products => _productRepository ??= new ProductRepository(_dbContext);

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public async Task RollbackChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (
            var entry in _dbContext.ChangeTracker
                .Entries()
                .Where(e => e.State != EntityState.Unchanged)
        )
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    await entry.ReloadAsync(cancellationToken);
                    break;
            }
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
