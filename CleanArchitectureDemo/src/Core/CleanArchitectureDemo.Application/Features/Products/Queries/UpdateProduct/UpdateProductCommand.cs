using CleanArchitectureDemo.Application.Common.Models;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    Guid CategoryId,
    string? Description = null,
    int StockQuantity = 0,
    string? ImageUrl = null
) : IRequest<Result<bool>>;
