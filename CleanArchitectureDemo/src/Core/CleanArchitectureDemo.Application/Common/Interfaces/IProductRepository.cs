using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Common.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(Guid categoryId);
        Task<Product?> GetProductWithCategoryAsync(Guid productId);
        Task<IReadOnlyList<Product>> GetInStockProductsAsync();
    }
}