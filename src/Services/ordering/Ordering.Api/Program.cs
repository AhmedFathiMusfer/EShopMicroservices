using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationSevcies(builder.Configuration);
builder.Services.AddInfrastructreSevcies(builder.Configuration);
builder.Services.AddApiSevcies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.UseApiSevcies();


app.Run();

