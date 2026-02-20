
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiSevcies(this IServiceCollection service)
        {
            return service;
        }
        public static WebApplication UseApiSevcies(this WebApplication app)
        {
            return app;
        }
    }
}