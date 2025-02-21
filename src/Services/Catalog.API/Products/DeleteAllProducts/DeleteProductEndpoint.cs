namespace Catalog.API.Products.DeleteAllProducts
{
    public record DeleteAllProductsResponse(bool IsSuccess = false, int DeletedCount = 0);

    public class DeleteAllProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapDelete("/products", Handle)
                .Produces<DeleteAllProductsResponse>()
                .WithName("DeleteAllProducts");

            static async Task<IResult> Handle(ISender sender)
            {
                DeleteAllProductsResult result = await sender.Send(new DeleteAllProductsQuery());
                DeleteAllProductsResponse response = result.Adapt<DeleteAllProductsResponse>();
                return Results.Ok(response);
            }
        }
    }
}