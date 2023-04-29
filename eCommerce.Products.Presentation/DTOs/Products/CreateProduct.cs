using Newtonsoft.Json.Linq;

namespace eCommerce.Products.Presentation.DTOs.Products;

public class CreateProduct
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public JObject? Characteristics { get; set; }
    public string? UnitOfMeassure { get; set; }
    public double? Price { get; set; }
    public bool? IsAvailable { get; set; }
    public double? Rating { get; set; }
    public IList<string> Categories { get; set; }
}
