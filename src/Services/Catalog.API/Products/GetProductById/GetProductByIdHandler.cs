﻿namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Guid productId = request.Id;
            Product? product = await session.LoadAsync<Product>(productId, cancellationToken) ?? throw new ProductNotFoundException("Products", request.Id);
            return new GetProductByIdResult(product);
        }
    }
}