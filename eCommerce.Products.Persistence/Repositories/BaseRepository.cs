﻿using eCommerce.Products.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Products.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T>
    where T : class
{
    private readonly ProductsDbContext _dbContext;

    public BaseRepository(ProductsDbContext dbContext) => (_dbContext) = (dbContext);

    public DbSet<T> Entity
    {
        get => _dbContext.Set<T>();
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetByCondition(
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken cancellationToken = default
    )
    {
        return await _dbContext.Set<T>().Where(conditionExpression).ToListAsync(cancellationToken);
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
}