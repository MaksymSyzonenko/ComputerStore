using ComputerStore.DefaultData;
using ComputerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.DataBaseInteraction
{
    public class ComputerStoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
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

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductID);

            modelBuilder.Entity<Review>()
                .HasKey(r => r.ReviewID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductID);

            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartID);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserID);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductID);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductID)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Cart>()
                .Property(c => c.CartID)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewID)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionID)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Product>()
                .HasData(DefaultProducts.Products);

            modelBuilder.Entity<User>()
                .HasData(DefaultUsers.Users);

            modelBuilder.Entity<Review>()
                .HasData(DefaultReviews.Reviews);
        }
    }
}
