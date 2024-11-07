using Microsoft.Extensions.Logging;
using System.Linq;
namespace Catalog.API.Products.GetProduct
{
    public record CreateProductQuery() 
        : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<CreateProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(CreateProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Handle Called {@Query}", request);
            var products = await session.Query<Product>().ToListAsync();
            return new GetProductResult(products);

        }
    }
}
