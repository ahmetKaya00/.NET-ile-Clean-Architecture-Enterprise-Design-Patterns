using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.Features.Categories.Queries.GetAllCategories;
using CleanArchitectureDemo.Application.Features.Categories.Queries.GetCategoryById;
using CleanArchitectureDemo.Application.Features.Categories.Commands.CreateCategory;


namespace CleanArchitectureDemo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : BaseApiController
{

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<CategoryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetCategoryByIdQuery(id));
        if (!result.IsSuccess)
            return NotFound(result);
        return Ok(result);

    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }
}