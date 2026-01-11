using CarService.Api.Endpoints;
using CarService.Application.Garages.Commands;
using CarService.Domain.Entities;
using CarService.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<Garage>, PasswordHasher<Garage>>();
builder.Services.AddScoped<GarageLoginHandler>();


const string corsPolicyName = "Web";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, p =>
        p.WithOrigins("http://localhost:5173")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors(corsPolicyName);

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapApiEndpoints();

app.UseHttpsRedirection();
app.Run();
