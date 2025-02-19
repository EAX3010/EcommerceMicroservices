#region

using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(Id => Id.Value, // Writing to the database
                Id => CustomerId.Of(Id)); // Reading from the database

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(255);
            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}