#region

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

#endregion

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            _ = builder.HasKey(o => o.Id);

            _ = builder.Property(o => o.Id).HasConversion(
                Id => Id.Value,
                Id => OrderId.Of(Id));

            _ = builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            _ = builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            _ = builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    _ = nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                });
            _ = builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    _ = addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });
            _ = builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    _ = addressBuilder.Property(a => a.FirstName)
                        .HasMaxLength(50)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.LastName)
                        .HasMaxLength(50)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.EmailAddress)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.AddressLine)
                        .HasMaxLength(180)
                        .IsRequired();

                    _ = addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    _ = addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            _ = builder.ComplexProperty(o => o.Payment, paymentBuilder =>
                {
                    _ = paymentBuilder.Property(p => p.CardHolderName)
                        .HasMaxLength(50);

                    _ = paymentBuilder.Property(p => p.CardNumber)
                        .HasMaxLength(24)
                        .IsRequired();

                    _ = paymentBuilder.Property(p => p.CardExpiration)
                        .HasMaxLength(10);

                    _ = paymentBuilder.Property(p => p.CardSecurityNumber)
                        .HasMaxLength(3);

                    _ = paymentBuilder.Property(p => p.PaymentMethod);
                });

            _ = builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            _ = builder.Ignore(o => o.TotalPrice);
        }
    }
}