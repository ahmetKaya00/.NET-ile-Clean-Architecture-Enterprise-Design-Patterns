using CleanArchitectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureDemo.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.StockQuantity).IsRequired();
        builder.Property(p => p.ImageUrl).HasMaxLength(500);
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Amount)
            .HasColumnName("Price")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

            priceBuilder.Property(p => p.Currency)
            .HasColumnName("Currency")
            .HasMaxLength(3)
            .IsRequired();

        });
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}