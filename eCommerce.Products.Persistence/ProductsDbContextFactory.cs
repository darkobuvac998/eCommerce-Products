using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eCommerce.Products.Persistence;

public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", true)
            .Build();

        var builder = new DbContextOptionsBuilder<ProductsDbContext>();
        var connectionString = configuration.GetConnectionString("Db");

        builder.UseNpgsql(connectionString);

        return new ProductsDbContext(builder.Options);
    }
}
