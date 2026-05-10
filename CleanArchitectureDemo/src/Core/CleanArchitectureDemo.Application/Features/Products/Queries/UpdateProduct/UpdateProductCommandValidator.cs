using FluentValidation;

namespace CleanArchitectureDemo.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Geçerli bir ürün ID'si giriniz.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(200)
            .WithMessage("Ürün adı en fazla 200 karakter olabilir.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Fiyat sıfırdan küçük olamaz.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Para birimi boş olamaz.")
            .Length(3)
            .WithMessage("Para birimi ISO 4217 formatında olmalıdır.");

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty)
            .WithMessage("Geçerli bir kategori ID'si giriniz.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stok miktarı sıfırdan küçük olamaz.");
    }
}
