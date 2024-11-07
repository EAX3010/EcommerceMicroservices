using Marten;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdQuery(Guid Id) : IQuery<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool isSuccess = false);

    internal class DeleteProductByIdQueryHandler(IDocumentSession session, ILogger<DeleteProductByIdQueryHandler> logger) 
        : IQueryHandler<DeleteProductByIdQuery, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductByIdQueryHandler.Handle Called {@Query}", request);

            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            session.Delete<Product>(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductByIdResult(true);
        }
    }
}