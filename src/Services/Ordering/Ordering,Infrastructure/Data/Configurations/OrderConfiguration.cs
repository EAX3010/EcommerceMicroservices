using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
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
