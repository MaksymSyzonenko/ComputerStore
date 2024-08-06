using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Configurations
{
    public sealed class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.Property(c => c.CartId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasKey(c => c.CartId);

            builder.HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
