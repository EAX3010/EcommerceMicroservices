namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return basket ?? throw new BasketNotFoundException($"Basket not found for user: {userName}");
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(basket);
        ArgumentException.ThrowIfNullOrWhiteSpace(basket.Username);

        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);
        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        if (basket is null) return false;
        session.Delete(basket);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}