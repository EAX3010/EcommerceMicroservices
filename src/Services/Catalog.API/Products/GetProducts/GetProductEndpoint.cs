namespace Catalog.API.Products.GetProduct;

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", Handle).Produces<GetProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetAllProducts");

        async Task<IResult> Handle([AsParameters] CreateProductQuery request, ISender sender)
        {
            var result = await sender.Send(request);

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        }
    }
}