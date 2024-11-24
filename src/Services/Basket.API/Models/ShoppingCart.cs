namespace Basket.API.Models;

public class ShoppingCart
{
    public ShoppingCart(string _Username)
    {
        Username = _Username;
    }

    public ShoppingCart()
    {
    }

    public string Username { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Count);
}