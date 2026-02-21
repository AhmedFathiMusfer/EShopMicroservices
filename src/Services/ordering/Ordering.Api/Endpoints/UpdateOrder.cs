


using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Commands.UpdateOrder;
using Ordering.Application.Dtos;

namespace Ordering.Api.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (ISender sender, UpdateOrderRequest request) =>
                      {
                          var command = request.Adapt<UpdateOrderCommand>();
                          var result = await sender.Send(command);
                          var response = result.Adapt<UpdateOrderResponse>();
                          return Results.Ok(response);
                      }).WithName("UpdateOrder")
                      .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status400BadRequest)
                       .ProducesProblem(StatusCodes.Status404NotFound)
                      .WithSummary("Update Order")
                      .WithDescription("Update Order");
        }
    }
}
