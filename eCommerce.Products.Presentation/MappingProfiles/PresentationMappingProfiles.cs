using AutoMapper;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Presentation.DTOs.Products;

namespace eCommerce.Products.Presentation.MappingProfiles;

internal class PresentationMappingProfiles : Profile
{
    public PresentationMappingProfiles()
    {
        CreateMap<CreateProduct, CreateProductCommand>();
        CreateMap<UpdateProduct, UpdateProductCommand>();
    }
}
