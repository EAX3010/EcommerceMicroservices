namespace Catalog.API.Products.DeleteAllProducts
{
    public record DeleteAllProductsQuery : IQuery<DeleteAllProductsResult>;

    public record DeleteAllProductsResult(bool IsSuccess = false, int DeletedCount = 0);

    internal class DeleteAllProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<DeleteAllProductsQuery, DeleteAllProductsResult>
    {
        public async Task<DeleteAllProductsResult> Handle(DeleteAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            // Get all products
            IReadOnlyList<Product> products = await session.Query<Product>().ToListAsync(cancellationToken);

            if (!products.Any())
                return new DeleteAllProductsResult(true, 0);

            // Delete all products
            foreach (Product? product in products) session.Delete<Product>(product);

            await session.SaveChangesAsync(cancellationToken);
            return new DeleteAllProductsResult(true, products.Count);
        }
    }
}