using eCommerce.Products.Application.Abstractions.Queries;
using MediatR;

namespace eCommerce.Products.Application.Abstractions.Handlers;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse> { }
