
using Carter;
using Catalog.Api.Model;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender, [AsParameters] GetProductsRequest requset) =>
            {
                var query = requset.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var respons = result.Adapt<GetProductsResponse>();
                return Results.Ok(respons);
            }).WithName("Get Product").Produces<GetProductsResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("Get Product").WithDescription("Get Product").WithDescription("Get Product");
        }
    }
}