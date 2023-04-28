using eCommerce.Products.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<IList<T>> PaginageListAsync<T>(
        this IQueryable<T> query,
        PaginateRequest paginateRequest,
        CancellationToken cancellation = default
    )
    {
        return await query
            .Skip((paginateRequest.PageNumber - 1) * paginateRequest.PageSize)
            .Take(paginateRequest.PageSize)
            .ToListAsync(cancellation);
    }
}
