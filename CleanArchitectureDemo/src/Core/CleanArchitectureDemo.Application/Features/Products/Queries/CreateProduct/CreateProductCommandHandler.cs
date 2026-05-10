using AutoMapper;
using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Product;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<ProductDto>.Failure("Kategori bulunamadı.");
        
        var price = Money.Create(request.Price, request.Currency);

        var product = Product.Create(
            request.Name,
            price,
            request.CategoryId,
            request.Description,
            request.StockQuantity,
            request.ImageUrl
        );
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var productDto = _mapper.Map<ProductDto>(product);
        return Result<ProductDto>.Success(productDto,"Ürün başarıyla oluşturuldu.");
    }
}