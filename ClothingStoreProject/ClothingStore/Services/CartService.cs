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

        public CartResponseDto GetByUserId(int userId)
        {
            var cart = GetOrCreateCart(userId);
            return MapToDto(cart);
        }

        public CartResponseDto AddItem(int userId, AddCartItemDto dto)
        {
            var variant = variantRepo.GetById(dto.VariantId);
            if (variant == null)
                return null;

            if (variant.stockQuantity < dto.Quantity)
                return null;

            var cart = GetOrCreateCart(userId);

            var existing = cartItemRepo.GetByCartAndVariant(cart.cartId, dto.VariantId);
            if (existing != null)
            {
                existing.quantity += dto.Quantity;
                cartItemRepo.Update();
            }
            else
            {
                var item = new CartItem
                {
                    cartId = cart.cartId,
                    variantId = dto.VariantId,
                    quantity = dto.Quantity
                };
                cartItemRepo.Add(item);
            }

            cart.updatedAt = DateTime.UtcNow;
            cartRepo.Update();

            return GetByUserId(userId);
        }

        public CartResponseDto UpdateItemQuantity(int userId, int cartItemId, UpdateCartItemDto dto)
        {
            var item = cartItemRepo.GetById(cartItemId);
            if (item == null || item.Cart.userId != userId)
                return null;

            var variant = variantRepo.GetById(item.variantId);
            if (variant != null && variant.stockQuantity < dto.Quantity)
                return null;

            item.quantity = dto.Quantity;
            cartItemRepo.Update();

            return GetByUserId(userId);
        }

        public bool RemoveItem(int userId, int cartItemId)
        {
            var item = cartItemRepo.GetById(cartItemId);
            if (item == null || item.Cart.userId != userId)
                return false;

            cartItemRepo.Delete(item);
            return true;
        }
        public bool ClearCart(int userId)
        {
            var cart = cartRepo.GetByUserId(userId);
            if (cart == null)
                return false;

            cartItemRepo.ClearCart(cart.cartId);
            return true;
        }
        private CartResponseDto MapToDto(Cart cart)
        {
            return new CartResponseDto
            {
                CartId = cart.cartId,
                UserId = cart.userId,
                UpdatedAt = cart.updatedAt,
                CartItems = cart.CartItems.Select(ci => new CartItemResponseDto
                {
                    CartItemId = ci.cartItemId,
                    VariantId = ci.variantId,
                    Quantity = ci.quantity,
                    Price = ci.ProductVariant?.price ?? 0
                }).ToList()
            };

        }
    }
}