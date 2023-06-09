﻿using eCommerce.Products.Application.Responses.ProductReviews;
using Newtonsoft.Json.Linq;

namespace eCommerce.Products.Application.Responses.Products;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public JObject? Characteristics { get; set; }
    public string UnitOfMeassure { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public double Rating { get; set; }
    public IList<string> Categories { get; set; }
    public IList<ProductReviewResponse> Reviews { get; set; }
}
