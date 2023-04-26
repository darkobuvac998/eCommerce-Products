using MediatR;

namespace eCommerce.Products.Application.Abstractions.Commands;

public interface ICommand : IRequest { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }
