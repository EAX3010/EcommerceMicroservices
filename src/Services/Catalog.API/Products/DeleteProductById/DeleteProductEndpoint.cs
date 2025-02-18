﻿namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductByIdResponse(bool IsSuccess = false);

public class DeleteProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", Handle).Produces<DeleteProductByIdResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .WithName("DeleteProductById");

        static async Task<IResult> Handle(Guid id, ISender sender)
        {
            var result = await sender.Send(new DeleteProductByIdQuery(id));
            var response = result.Adapt<DeleteProductByIdResponse>();
            return Results.Ok(response);
        }
    }
}