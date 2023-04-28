namespace eCommerce.Products.API.Configuration;

public sealed class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddApplicationPart(Presentation.AssemblyReference.Assembly);

        services.AddSwaggerGen();
    }
}
