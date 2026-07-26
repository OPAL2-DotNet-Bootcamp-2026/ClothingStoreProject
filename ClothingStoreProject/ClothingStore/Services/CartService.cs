using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class CartService
    {
        private CartRepo cartRepo;
        private CartItemRepo cartItemRepo;
        private ProductVariantRepo variantRepo;

        public CartService(CartRepo _cartRepo, CartItemRepo _cartItemRepo, ProductVariantRepo _variantRepo)
        {
            cartRepo = _cartRepo;
            cartItemRepo = _cartItemRepo;
            variantRepo = _variantRepo;
        }

    
        public Cart GetOrCreateCart(int userId)
        {
            var cart = cartRepo.GetByUserId(userId);
            if (cart == null)
            {
                cart = new Cart { userId = userId };
                cart = cartRepo.Add(cart);
            }
            return cart;
        }






    }
}