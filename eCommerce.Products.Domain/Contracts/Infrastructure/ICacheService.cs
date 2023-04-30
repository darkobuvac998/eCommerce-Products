namespace eCommerce.Products.Domain.Contracts.Infrastructure;

public interface ICacheService
{
    Task<T?> GetAllAsync<T>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);
}
