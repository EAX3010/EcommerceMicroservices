namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageUrl,
        double Price)
        : ICommand<UpdateProductResponse>;

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapPut("/products", Handle).Produces<UpdateProductResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("UpdateProduct");

            static async Task<IResult> Handle(UpdateProductRequest request, ISender sender)
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>();

                UpdateProductResult result = await sender.Send(command);

                UpdateProductResponse response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            }
        }
    }
}