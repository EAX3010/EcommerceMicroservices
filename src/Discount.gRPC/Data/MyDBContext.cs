﻿using Discount.gRPC.Models;
namespace Discount.gRPC.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
            [
                new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
            ]);

        }
        public DbSet<Coupon> Coupons { get; set; } = default!;
    }
}