using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(customerId => customerId.Value, // Writing to the database
            dbId => CustomerId.Of(dbId)); // Reading from the database

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(255);
            builder.HasIndex(c => c.Email).IsUnique();

        }
    }
}
