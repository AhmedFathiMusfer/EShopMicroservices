

using System;
using Carter;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.DeleteProduct
{
    public class DeleteProductEndPoint : ICarterModule
    {
        //  public record UpdateProductRequest(Guid id);
        public record DeleteProductRespons(bool isSuccess);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var respons = result.Adapt<DeleteProductRespons>();
                return Results.Ok(respons);


            }).WithName("delete Product").Produces<DeleteProductRespons>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("delete Product").WithDescription("delete Product").WithDescription("delete Product");
        }

    }
}