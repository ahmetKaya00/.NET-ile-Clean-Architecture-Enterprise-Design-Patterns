using CleanArchitectureDemo.Application;
using CleanArchitectureDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Clean Architecture Demo API",
        Version = "v1",
        Description = "An API demonstrating Clean Architecture principles in .NET",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Kaya",
            Email = "kaya@example.com"
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Architecture Demo API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
