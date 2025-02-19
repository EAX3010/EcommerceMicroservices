namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageUrl,
    double Price)
    : ICommand<CreateProductResponse>;

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //localhost:6060/Product/
        _ = app.MapPost("/products", Handle).Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("CreateProduct");

        static async Task<IResult> Handle(CreateProductRequest request, ISender sender)
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        }
    }
}