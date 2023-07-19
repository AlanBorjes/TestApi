using System;
using TestApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlDatabase");
builder.Services.AddDbContext<AplicationDbContext>( options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();

app.Run();


// Ctrl + Shift + p y buscamos "Reload Window"

// Crear migraciones y ejecutarlas
// Instalar 
// dotnet tool install --global dotnet-ef
// Crear migraciones
// dotnet ef migrations add initialMigration
// Ejecutar migraciones
// dotnet ef database update