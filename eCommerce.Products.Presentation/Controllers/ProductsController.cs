using AutoMapper;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Queries.ProductReviews;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Domain.Shared;
using eCommerce.Products.Infrastructure.Auth;
using eCommerce.Products.Presentation.DTOs.ProductReview;
using eCommerce.Products.Presentation.DTOs.Products;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Products.Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/api/[controller]")]
public sealed class ProductsController : ApiController
{
    public ProductsController(ISender sender, IMapper mapper)
        : base(sender, mapper) { }

    [HasPolicy(Policies.Products.View)]
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(
        [FromQuery] PaginateRequest? paginateRequest,
        CancellationToken cancellationToken
    )
    {
        var command = new GetProductsQuery(default, paginateRequest ?? default);

        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HasPolicy(Policies.Products.View)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var command = new GetProductByIdQuery(id);

        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HasPolicy(Policies.Products.Add)]
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromBody] CreateProduct request,
        CancellationToken cancellationToken
    )
    {
        var command = Mapper.Map<CreateProductCommand>(request);

        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HasPolicy(Policies.Products.Edit)]
    [HttpPut]
    public async Task<IActionResult> UpdateProductAsync(
        [FromBody] UpdateProduct request,
        CancellationToken cancellationToken
    )
    {
        var command = Mapper.Map<UpdateProductCommand>(request);

        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HasPolicy(Policies.Products.Delete)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        await Sender.Send(new DeleteProductCommand(id), cancellationToken);
        return Ok();
    }

    [HasPolicy(Policies.ProductReviewes.View)]
    [HttpGet("{id:int}/reviews")]
    public async Task<IActionResult> GetProductReviews(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        var command = new GetProductReviewsQuery(id);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HasPolicy(Policies.ProductReviewes.Add)]
    [HttpPost("{id:int}/reviews")]
    public async Task<IActionResult> CreateProductReviewAsync(
        [FromBody] CreateProductReview request,
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        request.ProductId = id;
        var command = Mapper.Map<CreateProductReviewCommand>(request);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HasPolicy(Policies.ProductReviewes.Edit)]
    [HttpPut("{productId:int}/reviews/{reviewId:int}")]
    public async Task<IActionResult> UpdateProductReviewAsync(
        [FromBody] UpdateProductReview request,
        int productId,
        int reviewId,
        CancellationToken cancellationToken
    )
    {
        request.ProductId = productId;
        request.ReviewId = reviewId;
        var command = Mapper.Map<UpdateProductReviewCommand>(request);

        var response = await Sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HasPolicy(Policies.ProductReviewes.Delete)]
    [HttpDelete("{productId:int}/review/{reviewId:int}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromRoute] int productId,
        [FromRoute] int reviewId,
        CancellationToken cancellationToken
    )
    {
        await Sender.Send(new DeleteProductReviewCommand(productId, reviewId), cancellationToken);
        return Ok();
    }
}
