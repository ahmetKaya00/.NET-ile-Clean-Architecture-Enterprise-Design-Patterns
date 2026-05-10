using CleanArchitectureDemo.Application.DTOs.Product;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;