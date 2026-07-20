using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore
{
    public class ClothingStoreContext : DbContext
    {
        public DbSet<User> users {  get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductVariant> productsVariant { get; set; }
        public DbSet<Review> reviews { get; set; }

        public ClothingStoreContext(DbContextOptions<ClothingStoreContext> options) : base(options)
        {
        }

    }
}
