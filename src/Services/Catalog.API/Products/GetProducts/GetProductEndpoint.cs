
namespace Catalog.API.Products.GetProduct
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", Handle).Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetAllProducts");

            async Task<IResult> Handle(ISender sender)
            {

                GetProductResult? result = await sender.Send(new CreateProductQuery());

                GetProductResponse response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);

            }
        }
    }
}
