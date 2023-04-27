using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Persistence.Context;

namespace eCommerce.Products.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ProductsDbContext dbContext)
        : base(dbContext) { }
}
