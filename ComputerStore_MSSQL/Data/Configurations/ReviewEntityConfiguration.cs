using ComputerStore_MSSQL.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ComputerStore.DefaultData;

namespace ComputerStore_MSSQL.Data.Configurations
{
    public sealed class ReviewEntityConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            builder.Property(r => r.ReviewId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasKey(r => r.ReviewId);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            builder.HasData(DefaultReviews.Reviews);
        }
    }
}
