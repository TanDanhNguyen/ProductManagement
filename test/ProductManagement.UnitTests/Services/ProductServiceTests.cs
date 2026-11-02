using Moq;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Entities;

namespace ProductManagement.UnitTests.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnProductDtos()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product("Keyboard", "KB-001", 500),
            new Product("Mouse", "MS-001", 200)
        };

        var repositoryMock = new Mock<IProductRepository>();

        repositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        var service = new ProductService(repositoryMock.Object);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);

        Assert.Equal("Keyboard", result[0].Name);
        Assert.Equal("KB-001", result[0].Sku);
        Assert.Equal(500, result[0].Price);

        Assert.Equal("Mouse", result[1].Name);
        Assert.Equal("MS-001", result[1].Sku);
        Assert.Equal(200, result[1].Price);
    }
}