using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Product;
using CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitectureDemo.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitectureDemo.Application.Features.Products.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await Mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await Mediator.Send(new GetProductByIdQuery(id));
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest(Result<ProductDto>.Failure("URL'deki id ile gövdedeki id eşleşmiyor."));

        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteProductCommand(id));
        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
}