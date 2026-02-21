
using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplictionDbContext dbContext) : ICommndHandler<UpdateOrderCommand
    , UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //update order entitu from command;
            //save to db;
            // retutn result;
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.orders.FindAsync([orderId], cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderWithNewValueeNewOrder(order, command.Order);
            dbContext.orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }
        private void UpdateOrderWithNewValueeNewOrder(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZibCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZibCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);
            order.Update(OrderName.Of(orderDto.OrderName), updatedShippingAddress, updatedBillingAddress, updatedPayment, orderDto.Status);
            foreach (var orderItemDto in orderDto.OrderItems)
            {
                order.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }
        }
    }
}