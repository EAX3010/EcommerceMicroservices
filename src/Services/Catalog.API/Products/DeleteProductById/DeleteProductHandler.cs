using Marten;
using Microsoft.Extensions.Logging;
using MediatR;
using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdQuery(Guid Id) : IQuery<DeleteProductByIdResult>;
    public record DeleteProductByIdResult(bool IsSuccess = false);

    internal class DeleteProductByIdQueryHandler(IDocumentSession session) 
        : IQueryHandler<DeleteProductByIdQuery, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException("Product", request.Id);

            session.Delete<Product>(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductByIdResult(IsSuccess: true);
        }
    }
}