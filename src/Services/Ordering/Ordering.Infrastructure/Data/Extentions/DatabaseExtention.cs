using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extentions
{
    public static class DatabaseExtention
    {
        public static async Task InitializeDatabase(this IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                if (context.Database.IsSqlServer())
                {
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    await SeedAsync(context);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }
        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await GenerateCustomers(context, 10);

            // Check and seed Products
            if (!context.Products.Any())
            {
                var products = new List<Product>
        {
            new Product
            {
                Id = ProductId.Of(Guid.NewGuid()),
                Name = "Laptop",
                Price = 999.99m,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTime.UtcNow,
                LastModifiedBy = "Seeder"
            },
            new Product
            {
                Id = ProductId.Of(Guid.NewGuid()),
                Name = "Smartphone",
                Price = 499.99m,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder",
                LastModified = DateTime.UtcNow,
                LastModifiedBy = "Seeder"
            }
        };

                await context.Products.AddRangeAsync(products);
            }

            // Check and seed Orders (with relationships)
            if (!context.Orders.Any())
            {
                var customer = context.Customers.FirstOrDefault(); // Assuming at least one customer exists.
                var product = context.Products.FirstOrDefault();   // Assuming at least one product exists.

                if (customer != null && product != null)
                {
                    var order = Order.Create(OrderId.Of(Guid.NewGuid()), customer.Id, OrderName.Of("First Order"),
                        Address.Of("John", "Doe", "x3010@outlook.com", "123 Main St", "CA", "USA", "90001"), 
                        Address.Of("John", "Doe", "x3010@outlook.com", "123 Main St", "CA", "USA", "90001"),
                        Payment.Of("4111111111111111", "John Doe", "12/25", "123", 1));
                    order.CreatedAt = DateTime.UtcNow;
                    order.CreatedBy = "Seeder";
                    order.LastModified = DateTime.UtcNow;
                    order.LastModifiedBy = "Seeder";
                    order.Add(product.Id, 1, product.Price);
                    

                    await context.Orders.AddAsync(order);
                    await context.OrderItems.AddRangeAsync(order.OrderItems);
                }
            }

            await context.SaveChangesAsync();
        }
        private static async Task GenerateCustomers(ApplicationDbContext context, int Count)
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
    }
}
