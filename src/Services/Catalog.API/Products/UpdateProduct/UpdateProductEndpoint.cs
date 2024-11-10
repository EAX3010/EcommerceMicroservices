
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageUrl, double Price)
        : ICommand<UpdateProductResponse>;
    public record UpdateProductResponse(bool IsSuccess);
 
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", Handle).Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("UpdateProduct");

            async Task<IResult> Handle(UpdateProductRequest request, ISender sender) 
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>();

                UpdateProductResult? result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);

            }
        }
    }
}
