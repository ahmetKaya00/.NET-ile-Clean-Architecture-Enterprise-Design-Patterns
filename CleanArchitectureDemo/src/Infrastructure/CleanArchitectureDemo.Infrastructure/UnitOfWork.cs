using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Infrastructure.Context;
using CleanArchitectureDemo.Infrastructure.Repositories;

namespace CleanArchitectureDemo.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products => _productRepository ??= new ProductRepository(_context);
    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
