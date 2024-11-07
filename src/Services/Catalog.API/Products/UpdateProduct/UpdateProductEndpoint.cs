
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageUrl, double Price)
        : ICommand<UpdateProductResponse>;
    public record UpdateProductResponse(bool IsSuccess);
 
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>();

                UpdateProductResult? result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            })
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithName("UpdateProduct")
            .WithSummary("Update Product")
            .WithDescription("Updates a product in the system with the specified details. Returns true if successful.");
        }
    }
}
