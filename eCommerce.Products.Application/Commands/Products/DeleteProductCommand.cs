using eCommerce.Products.Application.Abstractions.Commands;

namespace eCommerce.Products.Application.Commands.Products;

public sealed record DeleteProductCommand(int Id) : ICommand;
