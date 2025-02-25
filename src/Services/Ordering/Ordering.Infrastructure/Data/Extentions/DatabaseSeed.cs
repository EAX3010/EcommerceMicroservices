namespace Ordering.Infrastructure.Data.Extensions
{
    public class DatabaseSeed
    {
        private static readonly string[] FirstNames =
        {
            "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda",
            "William", "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica",
            "Thomas", "Sarah", "Charles", "Karen", "Wei", "Li", "Juan", "Ana", "Mohammed", "Fatima"
        };

        private static readonly string[] LastNames =
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Chen", "Wong", "Kim", "Singh",
            "Kumar", "Patel", "Ali", "Khan", "Wang", "Li", "Zhang", "Liu", "Yang", "Nguyen"
        };

        private static readonly string[] Cities =
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia",
            "San Antonio", "San Diego", "Dallas", "San Jose", "Toronto", "Vancouver",
            "London", "Paris", "Berlin", "Tokyo", "Sydney", "Singapore", "Dubai", "Mumbai"
        };

        private static readonly string[] Countries =
        {
            "USA", "Canada", "UK", "France", "Germany", "Japan", "Australia",
            "Singapore", "UAE", "India", "China", "Brazil", "Mexico", "Spain", "Italy"
        };

        private static readonly (string Category, string[] Products, decimal[] PriceRanges)[] ProductCategories =
        {
            ("Laptops", new[]
            {
                "MacBook Pro", "Dell XPS", "ThinkPad X1", "HP Spectre", "ROG Zephyrus",
                "Surface Laptop", "Acer Swift", "LG Gram", "Razer Blade", "MSI Stealth"
            }, new[] { 999.99m, 1499.99m, 1999.99m, 2499.99m, 2999.99m }),

            ("Smartphones", new[]
            {
                "iPhone Pro", "Galaxy S", "Pixel Pro", "OnePlus", "Xiaomi Mi",
                "OPPO Find", "Huawei P", "Sony Xperia", "Motorola Edge", "Nothing Phone"
            }, new[] { 699.99m, 899.99m, 1099.99m, 1299.99m, 1499.99m }),

            ("Accessories", new[]
            {
                "Pro Keyboard", "Gaming Mouse", "4K Monitor", "Wireless Earbuds", "USB-C Hub",
                "Power Bank", "Laptop Stand", "Webcam Pro", "Gaming Headset", "Portable SSD"
            }, new[] { 49.99m, 99.99m, 149.99m, 199.99m, 299.99m })
        };

        public static async Task GenerateCustomers(ApplicationDbContext context, int count)
        {
            if (!context.Customers.Any())
            {
                Random random = new Random();
                List<Customer> customers = new List<Customer>();

                for (int i = 0; i < count; i++)
                {
                    string firstName = FirstNames[random.Next(FirstNames.Length)];
                    string lastName = LastNames[random.Next(LastNames.Length)];
                    string email =
                        $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(100, 999)}@{GetRandomEmailDomain(random)}";

                    customers.Add(Customer.Create(
                        CustomerId.Of(Guid.NewGuid()),
                        $"{firstName} {lastName}",
                        email
                    ));
                }

                await context.Customers.AddRangeAsync(customers);
                _ = await context.SaveChangesAsync();
            }
        }

        public static async Task GenerateProducts(ApplicationDbContext context, int count)
        {
            if (!context.Products.Any())
            {
                Random random = new Random();
                List<Product> products = new List<Product>();

                foreach ((string Category, string[] Products, decimal[] PriceRanges) category in ProductCategories)
                {
                    int categoryCount = count / ProductCategories.Length;

                    for (int i = 0; i < categoryCount; i++)
                    {
                        string baseProduct = category.Products[random.Next(category.Products.Length)];
                        decimal basePrice = category.PriceRanges[random.Next(category.PriceRanges.Length)];

                        // Add slight price variation
                        decimal priceVariation = (decimal)(random.NextDouble() * 0.1 - 0.05); // ±5%
                        decimal finalPrice = Math.Round(basePrice * (1 + priceVariation), 2);

                        int year = random.Next(2023, 2026);
                        string productName = $"{baseProduct} {year}";

                        products.Add(Product.Create(productName, finalPrice));
                    }
                }

                await context.Products.AddRangeAsync(products);
                _ = await context.SaveChangesAsync();
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

                List<Customer> customers = await context.Customers.ToListAsync();
                List<Product> products = await context.Products.ToListAsync();
                Random random = new Random();
                List<Order> orders = new List<Order>();

                // Generate orders distributed over the last 12 months
                DateTime startDate = DateTime.Now.AddYears(-1);

                for (int i = 0; i < count; i++)
                {
                    Customer customer = customers[random.Next(customers.Count)];
                    DateTime orderDate = startDate.AddDays(random.Next(365));
                    string[] customerName = customer.Name.Split(' ');

                    // Generate realistic addresses
                    Address shippingAddress = GenerateRandomAddress(random);
                    Address billingAddress = random.Next(100) < 80 ? shippingAddress : GenerateRandomAddress(random);

                    Order order = Order.Create(
                        OrderId.Of(Guid.NewGuid()),
                        customer.Id,
                        OrderName.Of($"ORD-{orderDate:yyyyMMdd}-{random.Next(1000, 9999)}"),
                        shippingAddress,
                        billingAddress,
                        GenerateRandomPayment(random)
                    );

                    // Add products with realistic grouping
                    int categoryIndex = random.Next(ProductCategories.Length);
                    List<Product> categoryProducts = products
                        .Where(p => p.Name.StartsWith(ProductCategories[categoryIndex].Products[0]))
                        .OrderBy(_ => random.Next())
                        .Take(random.Next(1, 4))
                        .ToList();

                    foreach (Product? product in categoryProducts)
                    {
                        int quantity = GetRealisticQuantity(product.Name);
                        order.Add(product.Id, quantity, product.Price);
                    }

                    // Add random accessories if ordering main products
                    if (categoryIndex != 2) // If not already ordering accessories
                    {
                        List<Product> accessories = products
                            .Where(p => p.Name.Contains("Keyboard") || p.Name.Contains("Mouse") ||
                                        p.Name.Contains("Headset"))
                            .OrderBy(_ => random.Next())
                            .Take(random.Next(0, 3))
                            .ToList();

                        foreach (Product? accessory in accessories)
                        {
                            order.Add(accessory.Id, 1, accessory.Price);
                        }
                    }

                    orders.Add(order);
                }

                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    await context.Orders.AddRangeAsync(orders);
                    foreach (Order order in orders)
                    {
                        await context.OrderItems.AddRangeAsync(order.OrderItems);
                    }

                    _ = await context.SaveChangesAsync();
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

        private static string GetRandomEmailDomain(Random random)
        {
            string[] domains = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com" };
            return domains[random.Next(domains.Length)];
        }

        private static Address GenerateRandomAddress(Random random)
        {
            string firstName = FirstNames[random.Next(FirstNames.Length)];
            string lastName = LastNames[random.Next(LastNames.Length)];
            int streetNumber = random.Next(1, 9999);
            string[] streetTypes = new[] { "St", "Ave", "Blvd", "Rd", "Lane", "Drive" };
            string street =
                $"{streetNumber} {LastNames[random.Next(LastNames.Length)]} {streetTypes[random.Next(streetTypes.Length)]}";
            string city = Cities[random.Next(Cities.Length)];
            string country = Countries[random.Next(Countries.Length)];
            string zipCode = random.Next(10000, 99999).ToString();
            string email =
                $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(100, 999)}@{GetRandomEmailDomain(random)}";

            return Address.Of(firstName, lastName, email, street, city, country, zipCode);
        }

        private static Payment GenerateRandomPayment(Random random)
        {
            string[] cardTypes = new[] { "4", "5", "3", "6" }; // Representing different card types
            string cardNumber = cardTypes[random.Next(cardTypes.Length)] +
                             new string(Enumerable.Range(0, 15).Select(_ => random.Next(10).ToString()[0]).ToArray());
            string name = $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(LastNames.Length)]}";
            int year = DateTime.Now.Year + random.Next(1, 5);
            int month = random.Next(1, 13);
            string expiryDate = $"{month:D2}/{year % 100:D2}";
            string cvv = random.Next(100, 1000).ToString();

            return Payment.Of(cardNumber, name, expiryDate, cvv, random.Next(1, 5));
        }

        private static int GetRealisticQuantity(string productName)
        {
            // Most people buy 1 laptop/phone, but might buy multiple accessories
            if (productName.Contains("Laptop") || productName.Contains("Phone"))
            {
                return 1;
            }

            // Accessories might be bought in larger quantities
            Random random = new Random();
            return random.Next(1, 4);
        }
    }
}