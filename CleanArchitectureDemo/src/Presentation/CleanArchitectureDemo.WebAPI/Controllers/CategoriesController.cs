using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureDemo.Application.Common.Models;


namespace CleanArchitectureDemo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : BaseApiController
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return Ok(Result<List<CategoryDto>>.Success(categoryDtos));
    }
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound(Result<CategoryDto>.Failure("Category not found"));
        }
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return Ok(Result<CategoryDto>.Success(categoryDto));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var isUnique = await _unitOfWork.Categories.IsCategoryNameUniqueAsync(dto.Name);
        if (!isUnique)
        {
            return BadRequest(Result<CategoryDto>.Failure("Category name is not unique"));
        }
        var category = CleanArchitectureDemo.Domain.Entities.Category.Create(dto.Name);
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        var categoryDto = _mapper.Map<CategoryDto>(category);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, Result<CategoryDto>.Success(categoryDto, "Kategori başarıyla oluşturuldu"));
    }
}