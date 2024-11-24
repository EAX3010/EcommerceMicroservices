namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken);
        public Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken);
        public Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken);
    }
}
