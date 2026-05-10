using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Product;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId,
    string? Description = null,
    int StockQuantity = 0,
    string? ImageUrl = null
    ) : IRequest<Result<ProductDto>>;
