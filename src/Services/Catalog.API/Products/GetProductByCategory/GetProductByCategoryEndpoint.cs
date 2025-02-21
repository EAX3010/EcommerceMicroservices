namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Product);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/products/category/{category}", Handle).Produces<GetProductsByCategoryResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetProductsByCategory");

            static async Task<IResult> Handle(string category, ISender sender)
            {
                GetProductsByCategoryResult result = await sender.Send(new GetProductsByCategoryQuery(category));

                GetProductsByCategoryResponse response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            }
        }
    }
}