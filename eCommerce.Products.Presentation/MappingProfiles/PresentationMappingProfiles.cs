using AutoMapper;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Presentation.DTOs.ProductReview;
using eCommerce.Products.Presentation.DTOs.Products;

namespace eCommerce.Products.Presentation.MappingProfiles;

internal class PresentationMappingProfiles : Profile
{
    public PresentationMappingProfiles()
    {
        CreateMap<CreateProduct, CreateProductCommand>();
        CreateMap<UpdateProduct, UpdateProductCommand>();
        CreateMap<CreateProductReview, CreateProductReviewCommand>();
        CreateMap<UpdateProductReview, UpdateProductReviewCommand>();
    }
}
