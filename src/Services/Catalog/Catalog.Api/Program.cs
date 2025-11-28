using Carter;
using MediatR;
using Marten;
using FluentValidation;
using BuildingBlocks.Behavior;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten((opt) =>
{
    opt.Connection(builder.Configuration.GetConnectionString("connection-string") ?? "");
});
var app = builder.Build();


app.MapCarter();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
