using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carter;
using Catalog.Api.Model;
using Catalog.Api.Products.GetProducts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var respons = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(respons);
            }).WithName("Get Product by id").Produces<GetProductsResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).WithSummary("Get Product by id");
        }
    }
}