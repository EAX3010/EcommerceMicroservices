namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdResponse(bool IsSuccess = false);

    public class DeleteProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapDelete("/products/{id}", Handle).Produces<DeleteProductByIdResponse>()
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DeleteProductById");

            static async Task<IResult> Handle(Guid id, ISender sender)
            {
                DeleteProductByIdResult result = await sender.Send(new DeleteProductByIdQuery(id));
                DeleteProductByIdResponse response = result.Adapt<DeleteProductByIdResponse>();
                return Results.Ok(response);
            }
        }
    }
}