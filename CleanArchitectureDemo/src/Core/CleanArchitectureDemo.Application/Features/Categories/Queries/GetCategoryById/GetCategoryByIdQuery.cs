using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Category;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto>>;

