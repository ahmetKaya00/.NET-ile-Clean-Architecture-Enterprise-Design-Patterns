using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Product;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetProductWithCategoryAsync(request.Id);
        if (product is null)
            return Result<ProductDto>.Failure("Ürün bulunamadı.");
        var productDto = _mapper.Map<ProductDto>(product);
        return Result<ProductDto>.Success(productDto);
    }
}