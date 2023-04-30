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

    public IQueryable<Product> GetProductsDetails(
        Expression<Func<Product, bool>>? expression = default
    )
    {
        return expression is not null
            ? _dbContext.Products
                .Include(p => p.Categories)
                .Include(x => x.Reviews)
                .Where(expression)
            : _dbContext.Products.Include(p => p.Categories).Include(x => x.Reviews);
    }

    public IQueryable<Product> GetProductWithReviews(int id)
    {
        return _dbContext.Products.Include(p => p.Reviews).Where(p => p.Id == id);
    }
}
