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
        CreateMap<Product, ProductResponse>()
            .ForMember(
                x => x.Categories,
                m => m.MapFrom(x => x.Categories.Select(x => x.Name).ToList())
            );

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(x => x.Categories, m => m.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
