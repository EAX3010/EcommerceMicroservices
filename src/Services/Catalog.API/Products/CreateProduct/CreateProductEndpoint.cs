
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageUrl, double Price)
        : ICommand<CreateProductResponse>;
    public record CreateProductResponse(Guid Id);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                CreateProductCommand command = request.Adapt<CreateProductCommand>();

                CreateProductResult? result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);

            })
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithName("CreateProduct")
            .WithSummary("Create New Product")
            .WithDescription("Creates a new product in the system with the specified details. Returns the created product's information including its ID.");
        }
    }
}
