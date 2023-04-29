using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Products.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ProductsDbContext dbContext)
        : base(dbContext) { }

    public Task<IQueryable<Product>> GetProductsWithCategoryAsync(
        Expression<Func<Product, bool>>? expression,
        CancellationToken cancellationToken
    )
    {
        var query = expression is not null
            ? _dbContext.Products.Include(p => p.Categories).Where(expression)
            : _dbContext.Products.Include(p => p.Categories);

        return Task.FromResult(query);
    }
}
