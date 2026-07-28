using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repos
{
    public class CartItemRepo
    {
        private ClothingStoreContext context;

        public CartItemRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public CartItem? GetById(int id)
        {
            return context.CartItems
                .Include(ci => ci.Cart)
                .Include(ci => ci.ProductVariant)
                .FirstOrDefault(ci => ci.cartItemId == id);
        }

        public CartItem? GetByCartAndVariant(int cartId, int variantId)
        {
            return context.CartItems
                .FirstOrDefault(ci => ci.cartId == cartId && ci.variantId == variantId);
        }

        public void Add(CartItem cartItem)
        {
            context.CartItems.Add(cartItem);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(CartItem cartItem)
        {
            context.CartItems.Remove(cartItem);
            context.SaveChanges();
        }

        public void ClearCart(int cartId)
        {
            var items = context.CartItems.Where(ci => ci.cartId == cartId);
            context.CartItems.RemoveRange(items);
            context.SaveChanges();
        }
    }
}