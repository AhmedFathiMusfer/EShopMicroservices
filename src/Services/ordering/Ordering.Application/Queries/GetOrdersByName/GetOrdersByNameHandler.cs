

using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extentions;

namespace Ordering.Application.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplictionDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.orders.AsNoTracking().Include(o => o.OrderItems).Where(o => o.OrderName.Value.Contains(query.OrderName)).OrderBy(o => o.OrderName).ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}