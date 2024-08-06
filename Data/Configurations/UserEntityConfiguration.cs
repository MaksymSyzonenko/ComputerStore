using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ComputerStore_MSSQL.Data.Entities;
using ComputerStore.DefaultData;

namespace ComputerStore_MSSQL.Data.Configurations
{
    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasData(DefaultUsers.Users);
        }
    }
}
