using CleanArchitectureDemo.Domain.Common;

namespace CleanArchitectureDemo.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public string? Description { get; private set; }
    public int StockQuantity { get; private set; }
    public string? ImageUrl { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public Product() { }
    public static Product Create(string name, Money price, Guid categoryId, string? description = null, int stockQuantity = 0, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ürün adı boş olamaz.", nameof(name));
        if (price == null || price.Amount < 0)
            throw new ArgumentException("Geçersiz fiyat değeri.", nameof(price));
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Geçersiz kategori ID'si.", nameof(categoryId));

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            Description = description,
            StockQuantity = stockQuantity,
            ImageUrl = imageUrl,
            CategoryId = categoryId,
            CreatedDate = DateTime.UtcNow
        };
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Azaltılacak miktar sıfırdan büyük olmalıdır.", nameof(quantity));
        if (quantity > StockQuantity)
            throw new InvalidOperationException("Yetersiz stok.");

        StockQuantity -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Artırılacak miktar sıfırdan büyük olmalıdır.", nameof(quantity));
        StockQuantity += quantity;
    }

    public void UpdatePrice(decimal newPrice, string currency)
    {
        if (newPrice < 0)
            throw new ArgumentException("Geçersiz fiyat değeri.", nameof(newPrice));
        Price = Money.Create(newPrice, currency);
        LastModifiedDate = DateTime.UtcNow;
    }
}