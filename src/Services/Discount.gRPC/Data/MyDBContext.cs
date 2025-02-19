namespace Discount.gRPC.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasIndex(p => p.ProductName).IsUnique();
            });
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 },
                new Coupon
                {
                    Id = 3, ProductName = "Google Pixel 6", Description = "Google Pixel Discount", Amount = 120
                },
                new Coupon { Id = 4, ProductName = "OnePlus 9", Description = "OnePlus Discount", Amount = 90 },
                new Coupon { Id = 5, ProductName = "Sony Xperia 5", Description = "Sony Discount", Amount = 80 },
                new Coupon { Id = 6, ProductName = "LG Velvet", Description = "LG Discount", Amount = 70 },
                new Coupon { Id = 7, ProductName = "Motorola Edge", Description = "Motorola Discount", Amount = 60 },
                new Coupon { Id = 8, ProductName = "Huawei P40", Description = "Huawei Discount", Amount = 110 },
                new Coupon { Id = 9, ProductName = "Nokia 8.3", Description = "Nokia Discount", Amount = 50 },
                new Coupon { Id = 10, ProductName = "Xiaomi Mi 11", Description = "Xiaomi Discount", Amount = 85 },
                new Coupon { Id = 11, ProductName = "Oppo Find X3", Description = "Oppo Discount", Amount = 95 },
                new Coupon { Id = 12, ProductName = "Vivo X60", Description = "Vivo Discount", Amount = 65 },
                new Coupon { Id = 13, ProductName = "GT", Description = "GT Discount", Amount = 75 },
                new Coupon { Id = 14, ProductName = "Asus ROG Phone 5", Description = "Asus Discount", Amount = 130 },
                new Coupon
                {
                    Id = 15, ProductName = "Lenovo Legion Duel", Description = "Lenovo Discount", Amount = 115
                },
                new Coupon
                {
                    Id = 16, ProductName = "Black Shark 4", Description = "Black Shark Discount", Amount = 105
                },
                new Coupon { Id = 17, ProductName = "ZTE Axon 30", Description = "ZTE Discount", Amount = 55 },
                new Coupon
                {
                    Id = 18, ProductName = "Apple MacBook Pro", Description = "MacBook Discount", Amount = 200
                },
                new Coupon { Id = 19, ProductName = "Dell XPS 13", Description = "Dell Discount", Amount = 180 },
                new Coupon { Id = 20, ProductName = "HP x360", Description = "HP x360 Discount", Amount = 170 });
        }
    }
}