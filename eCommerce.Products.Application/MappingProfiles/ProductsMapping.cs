using AutoMapper;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Entities;

namespace eCommerce.Products.Application.MappingProfiles;

public sealed class ProductsMapping : Profile
{
    public ProductsMapping()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductResponse>();
        CreateMap<Product, GetProductResponse>();
    }
}
