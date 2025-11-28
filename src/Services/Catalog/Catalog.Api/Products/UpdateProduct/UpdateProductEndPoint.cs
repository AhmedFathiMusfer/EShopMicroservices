

using Carter;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.UpdateProduct
{
    public class UpdateProductEndPoint : ICarterModule
    {
        public record UpdateProductRequest(Guid id, string Name, string Description, List<string> Category, decimal Price);
        public record UpdateProductRespons(bool isSuccess);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var respons = result.Adapt<UpdateProductRespons>();
                return Results.Ok(respons);


            }).WithName("update Product").Produces<UpdateProductRespons>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).ProducesProblem(StatusCodes.Status404NotFound).WithSummary("update Product").WithDescription("update Product").WithDescription("update Product");
        }

    }
}