using eCommerce.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Products.Persistence.Context;

public class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
        : base(options) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (
            var entry in base.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
        )
        {
            entry.Entity.LastModifiedAt = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.Id, e.Code }).IsUnique();

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Code).IsRequired();

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.ProductCharacteristics).HasColumnType("jsonb");

            entity
                .HasMany(e => e.Images)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            entity
                .HasMany(e => e.Reviews)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(e => e.Categories)
                .WithMany(c => c.Products)
                .UsingEntity<ProductCategory>(
                    l => l.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId),
                    r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId)
                );
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.Property(e => e.Name).HasMaxLength(500);

            entity
                .HasMany(e => e.Products)
                .WithMany(e => e.Categories)
                .UsingEntity<ProductCategory>(
                    l => l.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId),
                    r => r.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId)
                );
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity
                .Property(e => e.ImageUrl)
                .IsRequired()
                .HasMaxLength(255)
                .HasConversion(b => b.OriginalString, a => new Uri(a))
                .HasColumnType("text");
        });

        modelBuilder.Entity<ProductReview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Review).HasMaxLength(500).IsRequired();
        });
    }
}
