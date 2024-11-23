namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    internal class GetBasketQueryHandler(IDocumentSession session)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {

            var ShoppingCart = await session.LoadAsync<ShoppingCart>(request.Username, cancellationToken);
            if (ShoppingCart == null)
                throw new BasketNotFoundException("Basket", request.Username);

            return new GetBasketResult(ShoppingCart);
        }
    }
}
