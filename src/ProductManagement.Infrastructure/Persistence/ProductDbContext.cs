using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence;

public sealed class ProductDbContext : DbContext
{
    public ProductDbContext(
        DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Sku)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(x => x.Sku)
                .IsUnique();

            entity.Property(x => x.Price)
                .HasPrecision(18, 2);

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();
        });
    }
}