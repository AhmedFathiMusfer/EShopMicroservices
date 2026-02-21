

using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extentions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplictionDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.orders.AsNoTracking().Include(o => o.OrderItems).Where(o => o.CustomerId == CustomerId.Of(query.customerId)).OrderBy(o => o.OrderName).ToListAsync(cancellationToken);
            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}