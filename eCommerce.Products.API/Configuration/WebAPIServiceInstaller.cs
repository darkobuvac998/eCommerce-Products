using eCommerce.Products.API.Middlewares;

namespace eCommerce.Products.API.Configuration;

public class WebAPIServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }
}
