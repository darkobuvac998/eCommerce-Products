using eCommerce.Products.Domain.Entities;
using System.Linq.Expressions;

namespace eCommerce.Products.Domain.Contracts.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IQueryable<Product>> GetProductsWithCategoryAsync(
        Expression<Func<Product, bool>>? expression,
        CancellationToken cancellationToken
    );
}
