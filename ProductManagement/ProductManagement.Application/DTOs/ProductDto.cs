namespace ProductManagement.Application.DTOs;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    decimal Price,
    DateTime CreatedAtUtc
);