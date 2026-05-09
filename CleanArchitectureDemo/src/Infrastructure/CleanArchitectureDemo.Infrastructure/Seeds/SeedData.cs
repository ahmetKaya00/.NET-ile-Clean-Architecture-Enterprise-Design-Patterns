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
    }
}
