using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureDemo.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await _dbSet.Where(p => p.CategoryId == categoryId)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductWithCategoryAsync(Guid productId)
    {
        return await _dbSet.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<IReadOnlyList<Product>> GetInStockProductsAsync()
    {
        return await _dbSet.Where(p => p.StockQuantity > 0)
            .AsNoTracking().ToListAsync();
    }
}