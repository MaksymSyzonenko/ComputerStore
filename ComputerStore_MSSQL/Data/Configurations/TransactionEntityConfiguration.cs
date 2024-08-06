using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore_MSSQL.Data.Configurations
{
    public sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.Property(t => t.TransactionId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasKey(t => t.TransactionId);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductId);
        }
    }
}
