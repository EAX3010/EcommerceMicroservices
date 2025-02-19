using Marten.Pagination;

namespace Catalog.API.Products.GetProduct;

public record CreateProductQuery(int? pageNumber = 1, int? pageSize = 10)
    : IQuery<GetProductResult>;

public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductQueryHandler(IDocumentSession session)
    : IQueryHandler<CreateProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(CreateProductQuery request, CancellationToken cancellationToken)
    {
        IPagedList<Product> products = await session.Query<Product>()
            .ToPagedListAsync(request.pageNumber ?? 1, request.pageSize ?? 10, cancellationToken);
        return new GetProductResult(products);
    }
}