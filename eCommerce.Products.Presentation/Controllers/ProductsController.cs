using eCommerce.Products.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Products.Domain.Shared;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Presentation.DTOs.Products;
using AutoMapper;

namespace eCommerce.Products.Presentation.Controllers;

[Route("/api/[controller]")]
public sealed class ProductsController : ApiController
{
    public ProductsController(ISender sender, IMapper mapper)
        : base(sender, mapper) { }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(
        [FromQuery] PaginateRequest paginateRequest,
        CancellationToken cancellationToken
    )
    {
        var command = new GetProductsQuery(default, paginateRequest);

        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProduct request)
    {
        var command = Mapper.Map<CreateProductCommand>(request);

        //var result = await Sender.Send(command);
        return Ok(command);
    }
}
