namespace ProductManagement.Domain.Entities;

public sealed class Product
{
    private Product() { } // EF Core

    public Product(string name, string sku, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required.");

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        Sku = sku.Trim().ToUpperInvariant();
        Price = price;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Sku { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
}