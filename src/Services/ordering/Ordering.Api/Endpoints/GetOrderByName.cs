
using System;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Queries.GetOrdersByName;

namespace Ordering.Api.Endpoints
{
    // public record GetOrderByNameRequest(string OrderName);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (ISender sender, string orderName) =>
                      {

                          var result = await sender.Send(new GetOrdersByNameQuery(orderName));
                          var response = result.Adapt<GetOrdersByNameResponse>();
                          return Results.Ok(response);
                      }).WithName("GetOrdersByNameRequest")
                      .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status400BadRequest)
                      .ProducesProblem(StatusCodes.Status404NotFound)
                      .WithSummary("Get Orders By Name")
                      .WithDescription("Get Orders By Name");
        }
    }
}