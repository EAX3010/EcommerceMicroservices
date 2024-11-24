namespace Catalog.API.Products.DeleteAllProducts;

public record DeleteAllProductsResponse(bool IsSuccess = false, int DeletedCount = 0);

public class DeleteAllProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products", Handle)
            .Produces<DeleteAllProductsResponse>()
            .WithName("DeleteAllProducts");

        async Task<IResult> Handle(ISender sender)
        {
            var result = await sender.Send(new DeleteAllProductsQuery());
            var response = result.Adapt<DeleteAllProductsResponse>();
            return Results.Ok(response);
        }
    }
}