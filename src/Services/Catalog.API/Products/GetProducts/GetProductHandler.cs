using Microsoft.Extensions.Logging;
using System.Linq;
namespace Catalog.API.Products.GetProduct
{
    public record CreateProductQuery() 
        : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductQueryHandler(IDocumentSession session)
        : IQueryHandler<CreateProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(CreateProductQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync();
            return new GetProductResult(products);

        }
    }
}
