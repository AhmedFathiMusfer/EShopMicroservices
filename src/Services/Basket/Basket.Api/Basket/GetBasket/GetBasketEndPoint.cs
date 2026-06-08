
using System.Security.Claims;
using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.GetBasket
{
    public record GetBasketRequset(string UserName);
    public record GetBasketResponse(ShoppingCard cart);


    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName}", async (ISender snder, string UserName, ClaimsPrincipal user) =>
            {
                var name = user.Identity?.Name;
                if (string.IsNullOrWhiteSpace(name) ||
                    !string.Equals(UserName, name, StringComparison.OrdinalIgnoreCase))
                {
                    return Results.Forbid();
                }

                var result = await snder.Send(new GetBasketQuery(UserName));
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            })
            .RequireAuthorization()
            .WithName("Get Basket by user name").Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound).WithDescription("Get Basket by user name");
        }
    }
}