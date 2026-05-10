using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Category;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return Result<List<CategoryDto>>.Success(categoryDtos);
    }
}