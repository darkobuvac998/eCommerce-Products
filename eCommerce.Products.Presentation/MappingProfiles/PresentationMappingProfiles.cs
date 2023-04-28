using AutoMapper;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Presentation.DTOs.Products;

namespace eCommerce.Products.Presentation.MappingProfiles;

internal class PresentationMappingProfiles : Profile
{
    public PresentationMappingProfiles()
    {
        CreateMap<CreateProduct, CreateProductCommand>()
            .ForMember(x => x.Characteristics, m => m.MapFrom(x => x.Characteristics.ToString()));
    }
}
