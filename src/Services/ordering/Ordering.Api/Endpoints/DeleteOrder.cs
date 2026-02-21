
//Accept order id 
// Map to deleteOrderCommand
// Sent to MediaTR
// Return result

using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Commands.DeleteOrder;

namespace Ordering.Api.Endpoints
{
    // public record DeleteOrderRequest(Guid OrderId);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{Id}", async (ISender sender, Guid Id) =>
                      {

                          var result = await sender.Send(new DeleteOrderCommand(Id));
                          var response = result.Adapt<DeleteOrderResponse>();
                          return Results.Ok(response);
                      }).WithName("DeleteOrder")
                      .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status400BadRequest)
                       .ProducesProblem(StatusCodes.Status404NotFound)
                      .WithSummary("Delete Order")
                      .WithDescription("Delete Order");
        }
    }
}