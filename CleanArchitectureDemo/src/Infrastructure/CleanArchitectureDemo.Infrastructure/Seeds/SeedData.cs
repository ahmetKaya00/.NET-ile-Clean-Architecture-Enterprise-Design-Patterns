using CleanArchitectureDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureDemo.Infrastructure.Seeds;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var elektronikId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var giyimId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        // EF Core HasData requires anonymous types when entity has private setters
        modelBuilder.Entity<Category>().HasData(
            new
            {
                Id = elektronikId,
                Name = "Elektronik",
                Description = (string?)"Elektronik urunler",
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = giyimId,
                Name = "Giyim",
                Description = (string?)"Giyim urunleri",
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            }
        );
        var laptopId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var telefonId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var tisortId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        modelBuilder.Entity<Product>().HasData(
            new
            {
                Id = laptopId,
                Name = "Laptop",
                Description = (string?)"Gaming laptop",
                StockQuantity = 50,
                ImageUrl = (string?)null,
                CategoryId = elektronikId,
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = telefonId,
                Name = "Akıllı Telefon",
                Description = (string?)"Son model akıllı telefon",
                StockQuantity = 100,
                ImageUrl = (string?)null,
                CategoryId = elektronikId,
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            },
            new
            {
                Id = tisortId,
                Name = "Tişört",
                Description = (string?)"Pamuklu tişört",
                StockQuantity = 200,
                ImageUrl = (string?)null,
                CategoryId = giyimId,
                CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = (string?)null,
                LastModifiedDate = (DateTime?)null,
                LastModifiedBy = (string?)null
            }
        );
        
        modelBuilder.Entity<Product>().OwnsOne(p => p.Price).HasData(
            new { ProductId = laptopId, Amount = 25000m, Currency = "TRY" },
            new { ProductId = telefonId, Amount = 15000m, Currency = "TRY" },
            new { ProductId = tisortId, Amount = 250m, Currency = "TRY" }
        );
    }
}
