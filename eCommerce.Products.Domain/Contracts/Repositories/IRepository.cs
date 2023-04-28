using eCommerce.Products.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Products.Domain.Contracts.Repositories;

public interface IRepository<T>
    where T : class
{
    DbSet<T> Entity { get; }
    Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ICollection<T>> GetByCondition(
        Expression<Func<T, bool>> conditionExpression,
        CancellationToken cancellationToken = default
    );
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);

    Task<IList<T>> GetPaginateReponseAsync(
        PaginateRequest paginateRequest,
        Expression<Func<T, bool>>? expression = default,
        CancellationToken cancellationToken = default
    );
}
