namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            _ = app.MapGet("/products/{id}", Handle).Produces<GetProductByIdResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithName("GetProductById");

            static async Task<IResult> Handle(Guid Id, ISender sender)
            {
                GetProductByIdResult result = await sender.Send(new GetProductByIdQuery(Id));

                GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }
        }
    }
}