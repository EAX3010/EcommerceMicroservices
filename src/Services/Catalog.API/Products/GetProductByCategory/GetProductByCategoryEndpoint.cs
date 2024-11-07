
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Product);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {

                var result = await sender.Send(new GetProductByCategoryQuery(category));

                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);

            })
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithName("GetProductsByCategory")
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category. Returns the products list.");
        }
    }
}
