
using BuildingBlocks.Authentication;
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
            service.AddJwtAuthentication(configuration, "ordering");
            service.AddExceptionHandler<CustomExceptionHandler>();
            service.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database"));
            return service;
        }
        public static WebApplication UseApiSevcies(this WebApplication app)
        {
            app.UseExceptionHandler(optins =>
            {
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapCarter();
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return app;
        }
    }
}