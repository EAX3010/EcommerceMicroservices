using Marten.Schema;

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

    [Identity] public string Username { get; set; } = default!;

    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal TotalPrice => Items.Sum(x => { return x.Price * x.Count; });

    public decimal TotalDiscount => Items.Sum(x => { return x.DiscountAmount * x.Count; });

    public decimal FinalPrice => Items.Sum(x => { return (x.Price - x.DiscountAmount) * x.Count; });
}