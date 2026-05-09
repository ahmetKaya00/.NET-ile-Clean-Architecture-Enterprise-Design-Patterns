using CleanArchitectureDemo.Domain.Entities;

namespace CleanArchitectureDemo.Application.Common.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);
        Task<bool> IsCategoryNameUniqueAsync(string name, Guid? excludeId = null);
    }
}