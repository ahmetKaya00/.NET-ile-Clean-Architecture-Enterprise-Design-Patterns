using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitectureDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Elektronik urunler", null, null, "Elektronik" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giyim urunleri", null, null, "Giyim" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "Currency", "CategoryId", "CreatedBy", "CreatedDate", "Description", "ImageUrl", "LastModifiedBy", "LastModifiedDate", "Name", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 25000m, "TRY", new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gaming laptop", null, null, null, "Laptop", 50 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 15000m, "TRY", new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Son model akıllı telefon", null, null, null, "Akıllı Telefon", 100 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 250m, "TRY", new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pamuklu tişört", null, null, null, "Tişört", 200 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
