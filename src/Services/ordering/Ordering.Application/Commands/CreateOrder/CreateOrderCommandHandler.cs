
using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IApplictionDbContext dbContext) : ICommndHandler<CreateOrderCommand
    , CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //create order entitu from command;
            //save to db;
            // retutn result;
            var order = CreateNewOrder(command.Order);
            dbContext.orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
        }
        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZibCode);
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZibCode);
            var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);
            var NewOrder = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId), OrderName.Of(orderDto.OrderName), shippingAddress, billingAddress, payment);
            foreach (var orderItemDto in orderDto.OrderItems)
            {
                NewOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }
            return NewOrder;

        }
    }
}