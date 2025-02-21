namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdQuery(Guid Id) : IQuery<DeleteProductByIdResult>;

    public record DeleteProductByIdResult(bool IsSuccess = false);

    internal class DeleteProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<DeleteProductByIdQuery, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            Product? product = await session.LoadAsync<Product>(request.Id, cancellationToken) ?? throw new ProductNotFoundException("Product", request.Id);
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductByIdResult(true);
        }
    }
}