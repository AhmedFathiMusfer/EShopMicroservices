
using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplictionDbContext dbContext) : ICommndHandler<DeleteOrderCommand
    , DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //Delete order entitu from command;
            //save to db;
            // retutn result;
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.orders.FindAsync([orderId], cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }
            dbContext.orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }

    }
}