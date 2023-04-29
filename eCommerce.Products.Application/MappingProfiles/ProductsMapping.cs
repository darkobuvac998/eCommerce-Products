using AutoMapper;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.Products;
using eCommerce.Products.Domain.Entities;

namespace eCommerce.Products.Application.MappingProfiles;

public sealed class ProductsMapping : Profile
{
    public ProductsMapping()
    {
        CreateMap<CreateProductCommand, Product>().ForMember(x => x.Categories, m => m.Ignore());
        CreateMap<Product, CreateProductResponse>()
            .ForMember(
                x => x.Categories,
                m => m.MapFrom(x => x.Categories.Select(x => x.Name).ToList())
            );
        CreateMap<Product, GetProductResponse>()
            .ForMember(
                x => x.Categories,
                m => m.MapFrom(x => x.Categories.Select(x => x.Name).ToList())
            );
    }
}
