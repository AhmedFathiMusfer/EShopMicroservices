
using System.Net;
using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCard Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (ISender sender, StoreBasketRequest request) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}", response);
            }).WithName("store basket").Produces<StoreBasketResponse>(StatusCodes.Status201Created).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("store basket").WithDescription("store basket").WithDescription("store basket"); ;
        }
    }
}