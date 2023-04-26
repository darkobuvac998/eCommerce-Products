using System.Linq.Expressions;

namespace eCommerce.Products.Domain.Contracts.Repositories;

public interface IRepository<T>
    where T : class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetByCondition(Expression<Func<T, bool>> conditionExpression);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}
