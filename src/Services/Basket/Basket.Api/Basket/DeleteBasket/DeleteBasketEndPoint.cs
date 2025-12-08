


using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.DeleteBasket
{
    /// public record DeleteBasketR(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketRsponse(bool ISsuccess);

    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(UserName));
                var response = result.Adapt<DeleteBasketRsponse>();
                return Results.Ok(response);
            }).WithName("delete basket").Produces<DeleteBasketRsponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("delete basket").WithDescription("delete basket").WithDescription("delete basket"); ;
        }
    }
}