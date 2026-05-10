using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Domain.Exceptions;
using MediatR;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

        if (product is null)
            throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

        await _unitOfWork.Products.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Ürün başarıyla silindi.");
    }
}
