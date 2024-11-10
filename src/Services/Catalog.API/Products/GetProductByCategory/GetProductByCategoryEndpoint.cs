namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Product);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", Handle).Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetProductsByCategory");
            async Task<IResult> Handle(string category, ISender sender)
            {

                var result = await sender.Send(new GetProductsByCategoryQuery(category));

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);

            }
        }
    }
}
