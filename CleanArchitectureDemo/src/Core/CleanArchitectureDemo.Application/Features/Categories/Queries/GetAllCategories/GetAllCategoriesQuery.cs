using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Category;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<Result<List<CategoryDto>>>
{
    
}