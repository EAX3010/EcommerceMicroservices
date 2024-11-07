
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Products.GetProduct;
using System.Linq;
namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Product);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle Called {@Query}", request);

            var category = request.category;
            var products = await session.Query<Product>().Where(p=>p.Category.Contains(category)).ToListAsync(cancellationToken);
            if (products == null)
                throw new ProductNotFoundException();

            return new GetProductByCategoryResult(products);
        }
    }
}
