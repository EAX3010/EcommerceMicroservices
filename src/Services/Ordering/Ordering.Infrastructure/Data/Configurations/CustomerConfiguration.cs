#region

using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id).HasConversion(Id => Id.Value, // Writing to the database
                Id => CustomerId.Of(Id)); // Reading from the database

            _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(100).IsRequired();

            _ = builder.Property(c => c.Email).HasMaxLength(255);
            _ = builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}