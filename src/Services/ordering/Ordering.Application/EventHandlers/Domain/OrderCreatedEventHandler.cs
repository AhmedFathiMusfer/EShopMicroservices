

using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extentions;
using Ordering.Domain.Events;

namespace Ordering.Application.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domian Evnet handled :{Doamian Event}", domainEvent.GetType().Name);
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedEvent = domainEvent.order.ToOrderDto();
                await publishEndpoint.Publish(orderCreatedEvent, cancellationToken);
            }

        }
    }
}