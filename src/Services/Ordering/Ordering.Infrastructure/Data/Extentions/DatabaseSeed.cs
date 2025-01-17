namespace Ordering.Infrastructure.Data.Extentions
{
    public class DatabaseSeed
    {
        public static async Task GenerateCustomers(ApplicationDbContext context, int Count)
        {
            // Check and seed Customers
            if (!context.Customers.Any())
            {
                Customer[] array = new Customer[Count];
                for (int i = 0; i < Count; i++)
                {
                    array[i] = Customer.Create(CustomerId.Of(Guid.NewGuid()), $"Customer-{i}", $"Customer-{i}@gmail.com");
                }
                await context.Customers.AddRangeAsync(array);
            }
            await context.SaveChangesAsync();
        }
        public static async Task GenerateProducts(ApplicationDbContext context, int count)
        {
            if (!context.Products.Any())
            {
                var productNames = new[]
                {
                   "Laptop", "Smartphone", "Tablet", "Monitor", "Keyboard",
                   "Mouse", "Headphones", "Speaker", "Webcam", "Charger"};

                var productPrices = new[]
                {
                   499.99m, 299.99m, 799.99m, 149.99m, 49.99m,
                   19.99m, 129.99m, 89.99m, 59.99m, 39.99m};

                var random = new Random();
                var products = new Product[count];

                for (int i = 0; i < count; i++)
                {
                    var name = productNames[random.Next(productNames.Length)];
                    var price = productPrices[random.Next(productPrices.Length)];

                    products[i] = Product.Create(name, price);
                }
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
        public static async Task GenerateOrders(ApplicationDbContext context, int count)
        {
            if (!context.Orders.Any())
            {
                if (!context.Customers.Any() || !context.Products.Any())
                {
                    Console.WriteLine("No customers or products available to generate orders.");
                    return;
                }

                var customers = context.Customers.ToList();  // Fetch all customers
                var products = context.Products.ToList();   // Fetch all products
                var random = new Random();

                var orders = new List<Order>();

                for (int i = 0; i < count; i++)
                {
                    // Select a random customer
                    var customer = customers[random.Next(customers.Count)];

                    // Create a new order
                    var order = Order.Create(
                        OrderId.Of(Guid.NewGuid()),
                        customer.Id,
                        OrderName.Of($"OrderID-{i + 1}"),
                        Address.Of("John", "Doe", "x3010@outlook.com", "123 Main St", "CA", "USA", "90001"),
                        Address.Of("John", "Doe", "x3010@outlook.com", "123 Main St", "CA", "USA", "90001"),
                        Payment.Of("4111111111111111", "John Doe", "12/25", "123", 1)
                    );

                    // Add random products to the order
                    int productCount = random.Next(1, 5); // Each order has between 1 and 4 products
                    var selectedProducts = products.OrderBy(_ => random.Next()).Take(productCount);

                    foreach (var product in selectedProducts)
                    {
                        var quantity = random.Next(1, 10); // Random quantity between 1 and 9
                        order.Add(product.Id, quantity, product.Price);
                    }

                    orders.Add(order);
                }

                // Save the generated orders and their items to the database
                using var transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    await context.Orders.AddRangeAsync(orders);
                    foreach (var order in orders)
                    {
                        await context.OrderItems.AddRangeAsync(order.OrderItems);
                    }
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error generating orders: {ex.Message}");
                    throw;
                }
            }

        }
    }
}
