
namespace Catalog.API.Products.GetProduct
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {

                GetProductResult? result = await sender.Send(new CreateProductQuery());

                GetProductResponse response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            })
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithName("GetProducts")
            .WithSummary("Get All Products")
            .WithDescription("Get all Products in system. Returns the all products.");
        }
    }
}
