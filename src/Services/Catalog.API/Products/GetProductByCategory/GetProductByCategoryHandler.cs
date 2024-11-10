
namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Product);
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryQueryHandler.Handle Called {@Query}", request);

            var category = request.category;
            var products = await session.Query<Product>().Where(p=>p.Category.Contains(category)).ToListAsync(cancellationToken);
            if (products == null)
                throw new ProductNotFoundException();

            return new GetProductsByCategoryResult(products);
        }
    }
}
