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
        
        public OrderDetailDto Checkout(int userId, CreateOrderDto dto)
        {
            var cart = cartRepo.GetByUserId(userId);
            if (cart == null || !cart.CartItems.Any())
                return null; // Controller translates -> BadRequest("Cart is empty.")

            foreach (var cartItem in cart.CartItems)
            {
                var variant = variantRepo.GetById(cartItem.variantId);
                if (variant == null || variant.stockQuantity < cartItem.quantity)
                    return null; // Controller translates -> BadRequest("Item out of stock: ...")
            }

            var order = new Order
            {
                userId = userId,
                orderDate = DateTime.UtcNow,
                status = OrderStatus.Pending,
                shippingAddress = dto.ShippingAddress,
                totalAmount = 0
            };
            order = orderRepo.Add(order); // Saved once here so orderId exists for the items below

            decimal total = 0;
            foreach (var cartItem in cart.CartItems)
            {
                var variant = variantRepo.GetById(cartItem.variantId);

                var orderItem = new OrderItem
                {
                    orderId = order.orderId,
                    variantId = cartItem.variantId,
                    quantity = cartItem.quantity,
                    unitPrice = variant.price // Snapshot at time of purchase
                };
                orderItemRepo.Add(orderItem);

                variant.stockQuantity -= cartItem.quantity;
                total += variant.price * cartItem.quantity;
            }

            order.totalAmount = total;
            orderRepo.Update(); // Single SaveChanges: OrderItems, stock changes, and totalAmount together

            cartItemRepo.ClearCart(cart.cartId);

            return GetById(order.orderId);
        }

        
    }
}
