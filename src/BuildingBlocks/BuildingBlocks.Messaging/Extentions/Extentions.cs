

using System.ComponentModel.Design;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BuildingBlocks.Messaging.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection service, IConfiguration configration, Assembly? assembly = null)
        {
            service.AddMassTransit(config =>
            {
                config.SetSnakeCaseEndpointNameFormatter();
                if (assembly != null)
                {
                    config.AddConsumers(assembly);
                }
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configration["MessageBroker:UserName"]);
                        host.Password(configration["MessageBroker:Password"]);

                    });
                    configurator.ConfigureEndpoints(context);
                });

            });

            return service;
        }
    }
}