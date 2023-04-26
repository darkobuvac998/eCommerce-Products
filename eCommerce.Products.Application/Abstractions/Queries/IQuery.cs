using MediatR;

namespace eCommerce.Products.Application.Abstractions.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
