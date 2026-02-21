

using System.Runtime.CompilerServices;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Extentions
{
    public static class OrderExtention
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            List<OrderDto> Result = [];
            foreach (var order in orders)
            {
                var ShippingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZibCode);
                var BillingAddress = new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZibCode);
                var Payment = new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod);
                var orderDto = new OrderDto(order.Id.Value, order.CustomerId.Value, order.OrderName.Value, ShippingAddress, BillingAddress, Payment, order.Status, order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList());

                Result.Add(orderDto);
            }
            return Result;

        }
    }
}