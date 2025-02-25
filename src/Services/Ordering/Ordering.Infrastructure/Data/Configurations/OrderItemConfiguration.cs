#region

using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            _ = builder.HasKey(oi => oi.Id);

            _ = builder.Property(oi => oi.Id).HasConversion(
                Id => Id.Value,
                Id => OrderItemId.Of(Id));

            _ = builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            _ = builder.Property(oi => oi.Quantity).IsRequired();

            _ = builder.Property(oi => oi.Price).IsRequired();
        }
    }
}