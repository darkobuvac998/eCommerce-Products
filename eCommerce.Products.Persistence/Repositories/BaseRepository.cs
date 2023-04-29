using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Products.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T>
    where T : class
{
    private DbSet<T>? _dbSet;
    protected readonly ProductsDbContext _dbContext;

    public BaseRepository(ProductsDbContext dbContext) => (_dbContext) = (dbContext);

    public DbSet<T> Entity
    {
        get => _dbSet ??= _dbContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public IQueryable<T> GetAll(CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<T>().AsQueryable<T>();
    }

    public Task RemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task<T> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task<IQueryable<T>> GetByConditionAsync(
        Expression<Func<T, bool>>? conditionExpression,
        CancellationToken cancellationToken = default
    )
    {
        return Task.FromResult(
            conditionExpression is not null
                ? _dbContext.Set<T>().Where(conditionExpression)
                : _dbContext.Set<T>()
        );
    }
}
