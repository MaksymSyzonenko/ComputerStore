using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ComputerStore.DefaultData;

namespace ComputerStore_MSSQL.Data.Configurations
{
    public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(e => e.ProductId);

            builder.Property(p => p.ProductId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasData(DefaultProducts.Products);
        }
    }
}
