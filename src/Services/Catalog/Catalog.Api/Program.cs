using Carter;
using MediatR;
using Marten;
using FluentValidation;
using BuildingBlocks.Authentication;
using BuildingBlocks.Behavior;
using BuildingBlocks.Exceptions.Handler;
using Catalog.Api.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwtAuthentication(builder.Configuration, "catalog");
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddMarten((opt) =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database") ?? "");
});
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database") ?? "");
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

app.UseHealthChecks("/healthChe", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

