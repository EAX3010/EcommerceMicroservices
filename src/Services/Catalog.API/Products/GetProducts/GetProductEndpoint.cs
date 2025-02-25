namespace Catalog.API.Products.GetProduct
{
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/products", Handle).Produces<GetProductResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetAllProducts");

            static async Task<IResult> Handle([AsParameters] CreateProductQuery request, ISender sender)
            {
                GetProductResult result = await sender.Send(request);

                GetProductResponse response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            }
        }
    }
}