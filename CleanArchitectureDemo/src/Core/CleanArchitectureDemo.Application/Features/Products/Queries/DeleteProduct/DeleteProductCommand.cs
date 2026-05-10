using CleanArchitectureDemo.Application.Common.Models;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<Result>;
