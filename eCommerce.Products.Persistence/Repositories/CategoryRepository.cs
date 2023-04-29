using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Persistence.Context;

namespace eCommerce.Products.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ProductsDbContext dbContext)
        : base(dbContext) { }
}
