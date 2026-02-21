
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
                option.AddOpenRequestPreProcessor(typeof(LoggingBehavior<,>));
                option.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });
            return service;
        }
    }
}