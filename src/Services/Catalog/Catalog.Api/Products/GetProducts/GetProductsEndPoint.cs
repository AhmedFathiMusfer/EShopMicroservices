
using Carter;
using Catalog.Api.Model;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var respons = result.Adapt<GetProductsResponse>();
                return Results.Ok(respons);
            }).WithName("Get Product").Produces<GetProductsResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("Get Product").WithDescription("Get Product").WithDescription("Get Product");
        }
    }
}