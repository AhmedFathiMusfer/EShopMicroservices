

using Carter;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.CreateProduct
{
    public class CreateProductEndPoint : ICarterModule
    {
        public record CreateProductRequest(string Name, string Description, List<string> Category, decimal Price);
        public record CreateProductRespons(Guid Id);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var respons = result.Adapt<CreateProductRespons>();
                return Results.Created($"/product/{respons.Id}", respons);


            }).WithName("createProduct").Produces<CreateProductRespons>(StatusCodes.Status201Created).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("Create Product").WithDescription("Create Product").WithDescription("Create Product");
        }

    }
}