using eCommerce.Products.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.API.Configuration;

public static class Extensions
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Failed applying DB migrations for DB {nameof(ProductsDbContext)}",
                    ex
                );
            }
        }

        return app;
    }
}
