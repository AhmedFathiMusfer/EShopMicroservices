
using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;

namespace Ordering.Application.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid customerId) : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

}