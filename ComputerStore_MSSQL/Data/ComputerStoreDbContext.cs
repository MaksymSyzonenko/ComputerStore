using ComputerStore_MSSQL.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ComputerStore_MSSQL.Data
{
    public sealed class ComputerStoreDbContext : IdentityDbContext<UserEntity>
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public ComputerStoreDbContext(DbContextOptions<ComputerStoreDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
