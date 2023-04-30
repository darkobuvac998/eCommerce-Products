using eCommerce.Products.Domain.Contracts;
using eCommerce.Products.Persistence.Context;
using eCommerce.Products.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.API.Configuration;

public sealed class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        var provider = services.BuildServiceProvider();
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

        services.AddDbContext<ProductsDbContext>(
            options =>
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Db"),
                        optionsBuilder =>
                        {
                            optionsBuilder
                                .MigrationsAssembly("eCommerce.Products.Persistence")
                                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        }
                    )
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(loggerFactory)
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
