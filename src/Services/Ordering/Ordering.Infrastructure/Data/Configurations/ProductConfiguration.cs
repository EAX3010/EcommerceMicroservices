#region

using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder.HasKey(p => p.Id);

            _ = builder.Property(p => p.Id).HasConversion(
                Id => Id.Value,
                Id => ProductId.Of(Id));

            _ = builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            _ = builder.Property(p => p.Price)
                .IsRequired();
        }
    }
}