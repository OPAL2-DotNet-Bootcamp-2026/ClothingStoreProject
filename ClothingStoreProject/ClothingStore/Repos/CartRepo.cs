using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repos
{
    public class CartRepo
    {
        private ClothingStoreContext context;

        public CartRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public Cart GetById(int id)
        {
            return context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .FirstOrDefault(c => c.cartId == id);
        }

        public Cart GetByUserId(int userId)
        {
            return context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .FirstOrDefault(c => c.userId == userId);
        }

        public Cart Add(Cart cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
            return cart;
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}