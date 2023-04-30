using eCommerce.Products.Application.Behaviors;
using MediatR;
using FluentValidation;

namespace eCommerce.Products.API.Configuration;

public sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssemblyContaining<Application.AssemblyReference>()
        );

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(
            Application.AssemblyReference.Assembly,
            includeInternalTypes: true
        );

        services.AddAutoMapper(Application.AssemblyReference.Assembly);
    }
}
