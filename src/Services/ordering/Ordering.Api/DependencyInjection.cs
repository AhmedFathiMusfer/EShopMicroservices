
using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiSevcies(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddCarter();
            service.AddExceptionHandler<CustomExceptionHandler>();
            service.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database"));
            return service;
        }
        public static WebApplication UseApiSevcies(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(optins =>
            {

            });
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    }
}