using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.Enums;

namespace ClothingStore.Services


{
    public class OrderService
    {
        private OrderRepo orderRepo;
        private OrderItemRepo orderItemRepo;
        private CartRepo cartRepo;
        private CartItemRepo cartItemRepo;
        private ProductVariantRepo variantRepo;
        
    }
}
