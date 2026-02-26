using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Behavior;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.Extentions;
using Carter;
using Discount.Grpc;
using FluentValidation;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

//Appliction_service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));


});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Data_service
builder.Services.AddMarten(mr =>
{
    mr.Connection(builder.Configuration.GetConnectionString("Database") ?? "");
    mr.Schema.For<ShoppingCard>().Identity(x => x.UserName);

}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//Grpc_service
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(builder.Configuration["GrpcSettings:Url"]!));
// Async Communcation Services
builder.Services.AddMessageBroker(builder.Configuration);
//Cross_cuting_service
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database") ?? "").AddRedis(builder.Configuration.GetConnectionString("Redis")!);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();

