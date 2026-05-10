using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Category;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category is null)
        {
            return Result<CategoryDto>.Failure("Category not found.");
        }

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return Result<CategoryDto>.Success(categoryDto);
    }
}