using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Domain.Entities;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);
        if (product is null)
            return Result<bool>.Failure("Ürün bulunamadı.");

        var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
        if (category is null)
            return Result<bool>.Failure("Belirtilen kategori bulunamadı.");

        var price = Money.Create(request.Price, request.Currency);
        product.UpdatePrice(price.Amount, price.Currency);

        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true, "Ürün başarıyla güncellendi.");
    }
}
