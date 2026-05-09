using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureDemo.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        return await _dbSet.Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<bool> IsCategoryNameUniqueAsync(string name, Guid? excludeId = null)
    {
        return !await _dbSet.AnyAsync(c => c.Name == name && (excludeId == null || c.Id != excludeId));
    }
}
