
using System.Security.Claims;
using Basket.Api.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.CheckoutBasket
{
    public record CheckoutBasketRequest(CheckoutBasketDto CheckoutBasketDto);
    public record CheckoutBasketResponse(bool IsScuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender, ClaimsPrincipal user) =>
            {
                var name = user.Identity?.Name;
                if (string.IsNullOrWhiteSpace(name) ||
                    !string.Equals(request.CheckoutBasketDto.UserName, name, StringComparison.OrdinalIgnoreCase))
                {
                    return Results.Forbid();
                }

                var command = request.Adapt<CheckoutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckoutBasketResponse>();
                return Results.Ok(response);
            })
            .RequireAuthorization();
        }
    }
}