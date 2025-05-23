﻿#region

using Marten.Schema;

#endregion

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using IDocumentSession session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync(cancellation))
            {
                return;
            }

            session.Store(PreProducts());
            await session.SaveChangesAsync(cancellation);
        }

        public static Product[] PreProducts()
        {
            return
            [
                new Product(Guid.NewGuid(), "iPhone 14 Pro", ["Smart Phone", "Apple"],
                    "Latest Apple flagship with dynamic island", "product-images/iphone14.png", 999),
                new Product(Guid.NewGuid(), "Samsung Galaxy S23", ["Smart Phone", "Android"],
                    "Premium Android experience with advanced camera system", "product-images/galaxy-s23.png", 899),
                new Product(Guid.NewGuid(), "MacBook Air M2", ["Laptop", "Apple"],
                    "Ultra-thin laptop with Apple Silicon", "product-images/macbook-air.png", 1299),
                new Product(Guid.NewGuid(), "Dell XPS 15", ["Laptop", "Windows"],
                    "Premium Windows laptop with 4K display", "product-images/dell-xps.png", 1799),
                new Product(Guid.NewGuid(), "iPad Pro 12.9", ["Tablet", "Apple"],
                    "Professional tablet with M2 chip", "product-images/ipad-pro.png", 1099),
                new Product(Guid.NewGuid(), "Sony WH-1000XM5", ["Headphones", "Audio"],
                    "Premium noise-cancelling headphones", "product-images/sony-headphones.png", 399),
                new Product(Guid.NewGuid(), "Apple Watch Series 8", ["Wearable", "Apple"],
                    "Advanced health and fitness tracking", "product-images/apple-watch.png", 399),
                new Product(Guid.NewGuid(), "LG C2 OLED 65\"", ["TV", "Electronics"],
                    "4K OLED TV with perfect blacks", "product-images/lg-oled.png", 1999),
                new Product(Guid.NewGuid(), "PlayStation 5", ["Gaming", "Console"],
                    "Next-gen gaming console", "product-images/ps5.png", 499),
                new Product(Guid.NewGuid(), "Canon EOS R6", ["Camera", "Photography"],
                    "Full-frame mirrorless camera", "product-images/canon-r6.png", 2499),
                new Product(Guid.NewGuid(), "Sonos Arc", ["Speaker", "Audio"],
                    "Premium Dolby Atmos soundbar", "product-images/sonos-arc.png", 899),
                new Product(Guid.NewGuid(), "Microsoft Surface Laptop 5", ["Laptop", "Windows"],
                    "Sleek touchscreen laptop", "product-images/surface-laptop.png", 1299),
                new Product(Guid.NewGuid(), "Google Pixel 7 Pro", ["Smart Phone", "Android"],
                    "Pure Android experience with top camera", "product-images/pixel-7.png", 899),
                new Product(Guid.NewGuid(), "Nintendo Switch OLED", ["Gaming", "Console"],
                    "Hybrid gaming console with OLED display", "product-images/switch-oled.png", 349),
                new Product(Guid.NewGuid(), "Samsung Galaxy Tab S8", ["Tablet", "Android"],
                    "Premium Android tablet with S Pen", "product-images/galaxy-tab.png", 699)
            ];
        }
    }
}