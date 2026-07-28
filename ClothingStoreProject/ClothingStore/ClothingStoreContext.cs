using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore
{
    public class ClothingStoreContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductsVariant { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ClothingStoreContext(DbContextOptions<ClothingStoreContext> options) : base(options)
        {
        }

    }
}
