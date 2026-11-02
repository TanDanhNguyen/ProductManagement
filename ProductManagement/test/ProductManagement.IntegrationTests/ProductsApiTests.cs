using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement.IntegrationTests;

public class ProductsApiTests
{
    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        using (var scope = factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<ProductDbContext>();

            await dbContext.Database.EnsureCreatedAsync();

            dbContext.Products.AddRange(
                new Product("Keyboard", "KB-001", 500),
                new Product("Mouse", "MS-001", 200)
            );

            await dbContext.SaveChangesAsync();
        }

        // Act
        var response = await client.GetAsync("/api/products");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var products = await response.Content
            .ReadFromJsonAsync<List<ProductDto>>();

        Assert.NotNull(products);
        Assert.Equal(2, products.Count);
        Assert.Contains(products, p => p.Sku == "KB-001");
        Assert.Contains(products, p => p.Sku == "MS-001");
    }
}