using CarService.Application.Cars.Commands;
using CarService.Application.Garages.Commands;
using CarService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/garages", async (CreateGarageCommand cmd, CreateGarageHandler handler, CancellationToken ct) =>
{
    var dto = await handler.Handle(cmd, ct);
    return Results.Created($"/garages/{dto.Id}", dto);
});
app.MapGet("/garages", async (GetGaragesHandler handler, CancellationToken ct) =>
{
    var dtos = await handler.Handle(ct);
    return Results.Ok(dtos);
});

app.MapGet("/garages/{id:guid}", async (Guid id, GetGarageByIdHandler handler, CancellationToken ct) =>
{
    var dto = await handler.Handle(id, ct);
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

app.MapPost("/garages/{garageId:guid}/cars", async (Guid garageId, CreateCarCommand cmd, CreateCarHandler handler, CancellationToken ct) =>
{
    var dto = await handler.Handle(cmd with { GarageId = garageId }, ct);
    return Results.Created($"/cars/{dto.Id}", dto);
});

app.UseHttpsRedirection();

app.Run();
