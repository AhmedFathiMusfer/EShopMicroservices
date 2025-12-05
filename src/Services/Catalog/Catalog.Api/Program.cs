using Carter;
using MediatR;
using Marten;
using FluentValidation;
using BuildingBlocks.Behavior;
using BuildingBlocks.Exceptions.Handler;



var builder = WebApplication.CreateBuilder(args);
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
    opt.Connection(builder.Configuration.GetConnectionString("connection-string") ?? "");
});
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();

