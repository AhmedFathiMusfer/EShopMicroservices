
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationSevcies(this IServiceCollection service)
        {
            return service;
        }
    }
}