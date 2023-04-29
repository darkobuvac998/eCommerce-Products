using eCommerce.Products.Domain.Contracts.Repositories;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Persistence.Context;

namespace eCommerce.Products.Persistence.Repositories;

public class ProductReviewRepository : BaseRepository<ProductReview>, IProductReviewRepository
{
    public ProductReviewRepository(ProductsDbContext dbContext)
        : base(dbContext) { }
}
