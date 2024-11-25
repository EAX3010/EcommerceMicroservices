
namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, HybridCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            return await cache.GetOrCreateAsync<ShoppingCart>($"{userName}",
                async cancel =>
                {
                    return await basketRepository.GetBasket(userName, cancel);

                }, cancellationToken: cancellationToken);

        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            ShoppingCart storedBasket = await basketRepository.StoreBasket(basket, cancellationToken);
            await cache.SetAsync(
                $"{basket.Username}",
                storedBasket,
                null,
                cancellationToken: cancellationToken);

            return storedBasket;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(userName, cancellationToken);
            bool isSuccess = await basketRepository.DeleteBasket(userName, cancellationToken);
            return isSuccess;
        }
    }
}
