
using System.Reflection;
using BuildingBlocks.Behavior;
using BuildingBlocks.Messaging.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationSevcies(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddMediatR(option =>
            {
                option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                option.AddOpenBehavior(typeof(ValidationBehavior<,>));
                option.AddOpenBehavior(typeof(LoggingBehavior<,>));


            });
            service.AddFeatureManagement();
            service.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
            return service;
        }
    }
}