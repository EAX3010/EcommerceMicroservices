

namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductsByCategoryQuery(string category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Product);
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {

            var category = request.category;
            var products = await session.Query<Product>().Where(p=>p.Category.Contains(category)).ToListAsync(cancellationToken);
            if (products == null || products?.Count == 0)
                throw new ProductNotFoundException("Products", request.category);

            return new GetProductsByCategoryResult(products!);
        }
    }
}
