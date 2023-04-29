using eCommerce.Products.Domain.Entities;
using System.Linq.Expressions;

namespace eCommerce.Products.Domain.Contracts.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> GetProductsWithCategories(
        Expression<Func<Product, bool>>? expression = default
    );

    IQueryable<Product> GetProductWithReviews(int id);
}
