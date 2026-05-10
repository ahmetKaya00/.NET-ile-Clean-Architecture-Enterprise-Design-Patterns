using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Category;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string? Description = null) : IRequest<Result<CategoryDto>>;