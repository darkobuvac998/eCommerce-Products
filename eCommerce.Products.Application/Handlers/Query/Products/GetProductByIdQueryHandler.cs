﻿using AutoMapper;
using eCommerce.Products.Application.Abstractions.Handlers;
using eCommerce.Products.Application.Queries.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Application.Shared;
using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Domain.Contracts.Infrastructure;
using eCommerce.Products.Domain.Entities;
using eCommerce.Products.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Application.Handlers.Query.Products;

public sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetProductByIdQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService
    ) => (_unitOfWork, _mapper, _cacheService) = (unitOfWork, mapper, cacheService);

    public async Task<ProductResponse> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var key = Utils.BuildCacheKey("product", $"{request.Id}");

        var cachedProduct = await _cacheService.GetAsync<Product>(key, cancellationToken);

        if (cachedProduct != null)
        {
            return _mapper.Map<ProductResponse>(cachedProduct);
        }

        var product =
            (
                await _unitOfWork.Products
                    .GetProductsDetails(p => p.Id == request.Id)
                    .FirstOrDefaultAsync()
            ) ?? throw new ItemNotFoundException(typeof(Product), request.Id);

        await _cacheService.SetAsync(key, product, cancellationToken);

        return _mapper.Map<ProductResponse>(product);
    }
}
