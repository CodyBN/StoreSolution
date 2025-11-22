using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StoreApi.API.Middleware;
using StoreApi.Application.Interfaces;
using StoreApi.Application.Services;
using StoreApi.Infrastructure;
using StoreApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
// DbContext - SQLite example (connection string in appsettings.json)
builder.Services.AddDbContext<StoreDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); 

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Store API";
        options.Theme = ScalarTheme.Purple;
    });
    //Handle cors for angular front end.
    app.UseCors(policy => policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200")   
        .AllowCredentials());
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();