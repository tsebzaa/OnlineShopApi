using Application;
using Application.Services;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DevSafeRossContext>(options =>
{
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
}
);

builder.Services.AddScoped<InterfaceService<Product>, ProductService>();
builder.Services.AddScoped<InterfaceRepository<Product>, ProductRepository>();
builder.Services.AddScoped<InterfaceService<Inventory>, InventoryService>();
builder.Services.AddScoped<InterfaceRepository<Inventory>, InventoryRepository>();
builder.Services.AddScoped<InterfaceService<ProductCategory>, ProductCategoryService>();
builder.Services.AddScoped<InterfaceRepository<ProductCategory>, ProductCategoryRepository>();
builder.Services.AddScoped<InterfaceService<User>, UserService>();
builder.Services.AddScoped<InterfaceRepository<User>, UserRepository>();
builder.Services.AddScoped<InterfaceService<PaymentType>, PaymentTypeService>();
builder.Services.AddScoped<InterfaceRepository<PaymentType>, PaymentTypeRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
