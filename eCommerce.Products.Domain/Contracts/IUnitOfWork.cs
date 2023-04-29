using eCommerce.Products.Domain.Contracts.Repositories;

namespace eCommerce.Products.Domain.Contracts;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    IProductReviewRepository ProductReviews { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task RollbackChangesAsync(CancellationToken cancellationToken = default);
    Task DisposeAsync();
}
