using System.Security.Claims;
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
            app.MapPost("/basket", async (ISender sender, StoreBasketRequest request, ClaimsPrincipal user) =>
            {
                var name = user.Identity?.Name;
                if (string.IsNullOrWhiteSpace(name))
                {
                    return Results.Unauthorized();
                }

                var command = request.Adapt<StoreBasketCommand>();
                command.Cart.UserName = name;

                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .RequireAuthorization()
            .WithName("store basket").Produces<StoreBasketResponse>(StatusCodes.Status201Created).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("store basket").WithDescription("store basket").WithDescription("store basket"); ;
        }
    }
}