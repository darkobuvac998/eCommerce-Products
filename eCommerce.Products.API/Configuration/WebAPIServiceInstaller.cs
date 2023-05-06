using eCommerce.Products.API.Middlewares;
using eCommerce.Products.API.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace eCommerce.Products.API.Configuration;

public class WebApiServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.AddTransient<GlobalExceptionHandlingMiddleware>();
    }
}
