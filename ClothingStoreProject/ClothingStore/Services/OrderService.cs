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
        
        public OrderService(OrderRepo _orderRepo, OrderItemRepo _orderItemRepo, CartRepo _cartRepo,
            CartItemRepo _cartItemRepo, ProductVariantRepo _variantRepo) //constructer 
        {
            orderRepo = _orderRepo;
            orderItemRepo = _orderItemRepo;
            cartRepo = _cartRepo;
            cartItemRepo = _cartItemRepo;
            variantRepo = _variantRepo;
        }
        public List<OrderListItemDto> GetAll()
        {
            return orderRepo.GetAll().Select(MapToListItemDto).ToList();
        }

        public List<OrderListItemDto> GetByUserId(int userId)
        {
            return orderRepo.GetByUserId(userId).Select(MapToListItemDto).ToList();
        }

        public OrderDetailDto GetById(int id)
        {
            var order = orderRepo.GetById(id);
            return order == null ? null : MapToDetailDto(order);
        }
    }
}
