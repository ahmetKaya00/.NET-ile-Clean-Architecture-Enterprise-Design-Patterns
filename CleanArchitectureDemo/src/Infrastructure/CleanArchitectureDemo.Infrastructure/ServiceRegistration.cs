using CleanArchitectureDemo.Application.Common.Interfaces;
using CleanArchitectureDemo.Domain.Entities;
using CleanArchitectureDemo.Infrastructure.Context;
using CleanArchitectureDemo.Infrastructure.Repositories;
using CleanArchitectureDemo.Infrastructure.Services;
using CleanArchitectureDemo.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitectureDemo.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
           options.Password.RequireDigit = true;
           options.Password.RequireLowercase = true;
           options.Password.RequireNonAlphanumeric = true;
           options.Password.RequireUppercase = true;
           options.Password.RequiredLength = 6;
           options.Password.RequiredUniqueChars = 1;
           options.User.RequireUniqueEmail = true;
           options.Lockout.MaxFailedAccessAttempts = 5;
           options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
           })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
