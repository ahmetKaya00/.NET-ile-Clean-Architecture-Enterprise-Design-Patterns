using FluentValidation;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.");
        
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");
        
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Para birimi boş olamaz.")
            .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");
        
        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty).WithMessage("Kategori ID boş olamaz.");
        
        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");
        
        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı sıfır veya daha büyük olmalıdır.");
        
        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("Resim URL'si en fazla 500 karakter olabilir.");
    }
}