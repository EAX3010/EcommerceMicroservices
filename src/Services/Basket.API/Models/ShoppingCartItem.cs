namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public string Color { get; set; } = default!;
        public int Count { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
    }
}