
using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Queries.GetOrders;

namespace Ordering.Api.Endpoints
{
    //  public record GetOrdersRequest(PaginationRequest PaginationRequest);
    public record GetOrdersResponse(PaginationResult<OrderDto> PaginationResult);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async (ISender sender, [AsParameters] PaginationRequest request) =>
                      {
                          var result = await sender.Send(new GetOrdersQuery(request));
                          var response = result.Adapt<GetOrdersResponse>();
                          return Results.Ok(response);
                      }).WithName("GetOrders")
                      .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status400BadRequest)
                      .WithSummary("Get Orders")
                      .WithDescription("Get Orders");
        }
    }
}