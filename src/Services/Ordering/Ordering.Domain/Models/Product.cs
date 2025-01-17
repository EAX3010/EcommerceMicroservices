namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public static Product Create(string name, decimal price)
        {
            var now = DateTime.UtcNow;

            return new Product
            {
                Id = ProductId.Of(Guid.NewGuid()),
                Name = name,
                Price = price,
            };
        }

    }
}
