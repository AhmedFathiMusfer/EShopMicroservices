
using System.Reflection;
using BuildingBlocks.Behavior;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationSevcies(this IServiceCollection service)
        {
            service.AddMediatR(option =>
            {
                option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                option.AddOpenBehavior(typeof(ValidationBehavior<,>));
                option.AddOpenBehavior(typeof(LoggingBehavior<,>));


            });
            return service;
        }
    }
}