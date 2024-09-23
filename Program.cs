using Microsoft.EntityFrameworkCore;
using GolrangTest.DataLayer.DbContexts;
using GolrangTest.Core;
using System;
using GolrangTest.Core.Services.Implementation;
using GolrangTest.Core.Services.Interfaces;
using GolrangTest.DataLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register ICommonRepository<TEntity>
builder.Services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));


// Register application services
builder.Services.AddScoped<IFactorService, FactorService>();
builder.Services.AddScoped<IFactorHeaderService, FactorHeaderService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

// Add controllers for Web API
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GolrangTest API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
