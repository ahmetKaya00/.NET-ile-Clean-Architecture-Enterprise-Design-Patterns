using FluentValidation;
using System.Net;
using CleanArchitectureDemo.Domain.Exceptions;
using CleanArchitectureDemo.Domain.Exeptions;
using System.Text.Json;

namespace CleanArchitectureDemo.WebAPI.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var(statusCode, title, error) = exception switch
        {
            ValidationException validationEx => (StatusCodes.Status400BadRequest, "Doğrulama Hatası", validationEx.Errors.Select(e => e.ErrorMessage).ToList()),

            NotFoundException notFoundEx => (StatusCodes.Status404NotFound, " Kayıt Bulunamadı", new List<string> { notFoundEx.Message }),

            DomainException domainEx => (StatusCodes.Status400BadRequest, "İş Kuralı Hatası", new List<string> { domainEx.Message }),
            UnauthorizedAccessException unauthorizedEx => (StatusCodes.Status401Unauthorized, "Yetkisiz Erişim", new List<string> { "Bu işlem için yetkiniz yok." }),

            _=>(StatusCodes.Status500InternalServerError, "Sunucu Hatası", new List<string> { "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz." })
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Sunucu hatası: {Message}", exception.Message);
        }
        else
        {
            _logger.LogWarning(exception, "İstemci hatası: {Message}", exception.Message);
        }

        var response = new
        {
            StatusCode = statusCode,
            Title = title,
            Errors = error
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}