using Marten.Schema;

namespace Basket.API.Models
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Count);
        public ShoppingCart(string _Username)
        {
            Username = _Username;
        }

        public ShoppingCart()
        {
        }
    }
}