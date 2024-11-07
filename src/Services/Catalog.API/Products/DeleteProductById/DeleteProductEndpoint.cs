using Catalog.API.Products.GetProductById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdResponse(bool isSuccess = false);

    public class DeleteProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new DeleteProductByIdQuery(id));
                var response = result.Adapt<DeleteProductByIdResponse>();
                return Results.Ok(response);

            })
            .Produces<DeleteProductByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Products")
            .WithName("DeleteProductById")
            .WithSummary("Delete Product By Id")
            .WithDescription("Deletes a product by its unique identifier. Returns true if successful.");
        }
    }
}