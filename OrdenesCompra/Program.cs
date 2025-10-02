using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using OrdenesCompra.Extensions;
using OrdenesCompra.Models;
using Persistence;
using Persistence.Repositories;
using Service.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Service extensions
builder.Services.ConfigureCors();

// Add services to the container.

// DbContexts
builder.Services.AddDbContext<RepositoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// EF Core conection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories y Services
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
