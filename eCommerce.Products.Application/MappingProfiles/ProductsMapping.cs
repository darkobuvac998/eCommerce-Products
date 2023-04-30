using AutoMapper;
using eCommerce.Products.Application.Commands.ProductReviews;
using eCommerce.Products.Application.Commands.Products;
using eCommerce.Products.Application.Responses.ProductReviews;
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
            )
            .ForMember(
                x => x.Reviews,
                m =>
                    m.MapFrom(
                        x =>
                            x.Reviews.Select(
                                r =>
                                    new ProductReviewResponse
                                    {
                                        Id = r.Id,
                                        Review = r.Review,
                                        UserId = r.UserId,
                                        Username = r.Username
                                    }
                            )
                    )
            );

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(x => x.Categories, m => m.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ProductReview, ProductReviewResponse>();

        CreateMap<CreateProductReviewCommand, ProductReview>();
        CreateMap<ProductReview, ProductReviewResponse>();
        CreateMap<UpdateProductReviewCommand, ProductReview>();
    }
}
