using Microsoft.EntityFrameworkCore;
using CleanArchitectureDemo.Domain.Common;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Infrastructure.Seeds;

namespace CleanArchitectureDemo.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        SeedData.Seed(modelBuilder);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}