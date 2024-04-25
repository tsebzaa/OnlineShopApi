using Application;
using Infrastructure;
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

builder.Services.AddScoped<InterfaceProductService, ProductService>();
builder.Services.AddScoped<InterfaceProductRepository, ProductRepository>();
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
