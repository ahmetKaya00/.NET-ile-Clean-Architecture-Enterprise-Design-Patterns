using CleanArchitectureDemo.Application.Common.Models;
using CleanArchitectureDemo.Application.DTOs.Auth;

namespace CleanArchitectureDemo.Application.Common.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> LoginAsync(string email, string password);
    Task<Result<AuthResponse>> RegisterAsync(string email, string password, string firstName, string lastName);
    Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken);
    Task<Result>RevokeTokenAsync(string userId);
}