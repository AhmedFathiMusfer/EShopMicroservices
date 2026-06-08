

using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Application.Dtos;

namespace Ordering.Api.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (ISender sender, CreateOrderRequest request) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.Id}", response);
            })
            .RequireAuthorization()
            .WithName("CreatOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}