

using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Application.Dtos;

namespace Ordering.Application.EventHandlers.Integration
{

    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Evnet handled :{Integration Event}", context.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);

        }
        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZibCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();
            var OrderDto = new OrderDto(orderId, message.CustomerId, message.UserName, addressDto, addressDto, paymentDto, Ordering.Domain.Enums.OrderStatus.Pending,
            [
                new OrderItemDto(orderId,new Guid("c3f8a123-5b77-4d11-8a22-2c9d9d1c2302"),5,500),
                new OrderItemDto(orderId,new Guid("d4e9b234-6c88-4e22-9b33-3d1e2e2d3403"),2,1100),

            ]
            );

            return new CreateOrderCommand(OrderDto);
        }
    }
}